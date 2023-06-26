using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Loading;
using UnityEngine;
using System.Linq;
using Roguelike.Assets.Scripts.Enemies;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string ContainerName = "Rooms";
        private const int MinExitCount = 3;

        private IEnemyFactory _enemyFactory;
        private ISceneLoadingService _sceneLoadingService;
        private IPersistentDataService _persistentDataService;
        private List<Room> _arenas;
        private StageStaticData _data;
        private Transform _roomContainer;
        private FinishRoom _finishRoom;
        private ExitPoint _connectingPoint;
        private Room _currentCorridor;
        private Room _currentRoom;
        private int _arenaRoomsCount;
        private int _bonusRoomMaxCount;
        private int _totalWeightRoom;

        public void Construct(StageStaticData stageData, IPersistentDataService persistentDataService,
            ISceneLoadingService sceneLoadingService, IEnemyFactory enemyFactory)
        {
            _data = stageData;
            _arenaRoomsCount = stageData.ArenasCount;
            _bonusRoomMaxCount = stageData.BonusRoomsMaxCount;
            _sceneLoadingService = sceneLoadingService;
            _persistentDataService = persistentDataService;
            _enemyFactory = enemyFactory;
        }

        public void BuildLevel()
        {
            _arenas = new List<Room>();
            _roomContainer = new GameObject(ContainerName).transform;

            ConnectStartRoom();

            if (_arenaRoomsCount > 0)
            {
                ConnectArenaRoom();
                GenerateArenas();
            }

            ConnectFinishRoom();
            GenerateBonusRoom();

            foreach (var arena in _arenas)
            {
                arena.HideExit();
            }
        }

        private void GenerateBonusRoom()
        {
            while (_arenas.Count > 0)
            {
                if (_bonusRoomMaxCount > 0)
                {
                    _currentRoom = _arenas[Random.Range(0, _arenas.Count)];

                    _currentRoom.UpdateValidExits();

                    if (_currentRoom.ValidExitCount > 0)
                        ConnectBonusRoom();
                    else
                        _arenas.Remove(_currentRoom);
                }
                else
                {
                    break;
                }
            }
        }

        private void GenerateArenas()
        {
            while (_arenaRoomsCount > 0)
            {
                while (_currentRoom.ValidExitCount > 0)
                {
                    ConnectArenaRoom();

                    if (_arenaRoomsCount <= 0)
                        break;
                }
            }
        }

        private void ConnectCorridor()
        {
            _connectingPoint = _currentRoom.SelectExitPoint();
            _currentCorridor = CreateRoom(_currentRoom, _data.Corridor, _connectingPoint);
        }

        private void ConnectArenaRoom()
        {
            ConnectCorridor();

            Room arenaRoom = CreateRoom(_currentCorridor, _data.Arenas);

            if (arenaRoom.gameObject.TryGetComponent<EnemySpawner>(out EnemySpawner enemySpawner))
                enemySpawner.Init(_enemyFactory, _persistentDataService.PlayerProgress,
                    _data.MinComplexityMultiplication, _data.MaxComplexityMultiplication, _data.Spawner);

            _arenas.Add(arenaRoom);

            if (_currentRoom.ValidExitCount <= 0)
            {
                _currentRoom.HideExit();

                _currentRoom = arenaRoom;
            }

            _arenaRoomsCount--;
        }

        private Room GetValidArenaRoom()
        {
            if (_bonusRoomMaxCount < _arenaRoomsCount)
            {
                return CreateRoom(_currentCorridor, _data.Arenas);
            }
            else
            {
                List<Room> validRoom = new();

                foreach (var room in _data.Arenas)
                {
                    if (room.ExitCount >= MinExitCount)
                    {
                        validRoom.Add(room);
                    }
                }

                if (validRoom.Count == 0)
                    return CreateRoom(_currentCorridor, _data.Arenas);

                return CreateRoom(_currentCorridor, validRoom);
            }
        }

        private void ConnectBonusRoom()
        {
            ConnectCorridor();
            CreateRoom(_currentCorridor, _data.BonusRoom);

            _bonusRoomMaxCount--;
        }

        private void ConnectStartRoom()
        {
            _currentRoom = Instantiate(_data.StartRoom, _roomContainer);
            _currentRoom.FillValidExits();
        }

        private void ConnectFinishRoom()
        {
            ConnectCorridor();

            _currentRoom.HideExit();

            _currentRoom = CreateRoom(_currentCorridor, _data.TransitionRoom);

            _finishRoom = _currentRoom.GetComponent<FinishRoom>();
            _finishRoom.SetNextLevel(_data.NextStageId, _persistentDataService);

            _finishRoom.PlayerFinishedLevel += GenerateNextLevel;

            if (_currentRoom.TryGetComponent<BossSpawner>(out BossSpawner spawner))
            {
                spawner.Init(_enemyFactory, _persistentDataService);
                _currentRoom.OpenDoor();
            }

            _currentRoom.HideExit();
        }

        private void GenerateNextLevel()
        {
            _finishRoom.PlayerFinishedLevel -= GenerateNextLevel;
            _sceneLoadingService.Load(_persistentDataService.PlayerProgress.WorldData.CurrentLevel);
        }

        private Room CreateRoom(Room exitRoom, List<Room> roomType, ExitPoint currentExitPoint = null)
        {
            Room nextRoom = GetRandomRoom(roomType);

            if (currentExitPoint == null)
                currentExitPoint = exitRoom.SelectExitPoint();

            nextRoom = Instantiate(nextRoom, currentExitPoint.transform.position, Quaternion.identity);

            nextRoom.Init(currentExitPoint);

            nextRoom.transform.SetParent(_roomContainer, true);

            return nextRoom;
        }

        private Room GetRandomRoom(List<Room> roomType)
        {
            _totalWeightRoom = roomType.Sum(x => x.SpawnWeight);

            int rool = Random.Range(0, _totalWeightRoom);

            foreach (Room room in roomType)
            {
                rool -= room.SpawnWeight;

                if (rool <= 0)
                    return room;
            }

            return null;
        }
    }
}