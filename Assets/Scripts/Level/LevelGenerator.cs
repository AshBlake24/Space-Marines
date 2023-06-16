using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Loading;
using UnityEngine;
using System.Linq;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string ContainerName = "Rooms";
        private const int MinExitCount = 2;

        private IEnemyFactory _enemyFactory;
        private ISceneLoadingService _sceneLoadingService;
        private IPersistentDataService _persistentDataService;
        private StageStaticData _data;
        private Transform _roomContainer;
        private FinishLevelTriger _enterTriger;
        private ExitPoint _connectingPoint;
        private Room _currentCorridor;
        private Room _currentRoom;
        private int _arenaRoomsCount;
        private int _bonusRoomCount;
        private int _totalWeightRoom;

        public void Construct(StageStaticData stageData, IPersistentDataService persistentDataService,
            ISceneLoadingService sceneLoadingService, IEnemyFactory enemyFactory)
        {
            _data = stageData;
            _arenaRoomsCount = stageData.ArenasCount;
            _bonusRoomCount = stageData.TreasureRoomsCount;
            _sceneLoadingService = sceneLoadingService;
            _persistentDataService = persistentDataService;
            _enemyFactory = enemyFactory;
        }

        public void BuildLevel()
        {
            _roomContainer = new GameObject(ContainerName).transform;

            ConnectStartRoom();

            while (_arenaRoomsCount > 0)
            {
                ConnectCorridor();
                ConnectArenaRoom();

                while (_currentRoom.ExitCount > 1)
                {
                    if (_bonusRoomCount > 0)
                    {
                        ConnectCorridor();
                        ConnectBonusRoom();

                        _bonusRoomCount--;
                    }
                    else
                    {
                        break;
                    }
                }

                _arenaRoomsCount--;
            }

            ConnectCorridor();
            ConnectFinishRoom();
        }

        private void ConnectCorridor()
        {
            _connectingPoint = _currentRoom.SelectExitPoint();
            _currentCorridor = CreateRoom(_currentRoom, _data.Corridor, _connectingPoint);
        }

        private void ConnectArenaRoom()
        {
            Room arenaRoom = GetValidArenaRoom();

            if (arenaRoom.gameObject.TryGetComponent<EnemySpawner>(out EnemySpawner enemySpawner))
                enemySpawner.Init(_enemyFactory, _persistentDataService.PlayerProgress, 
                    _data.MinComplexityMultiplication, _data.MaxComplexityMultiplication, _data.Spawner);

            _currentRoom.HideExit();

            _currentRoom = arenaRoom;
        }

        private Room GetValidArenaRoom()
        {
            if (_bonusRoomCount < _arenaRoomsCount)
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

                return CreateRoom(_currentCorridor, validRoom);
            }
        }

        private void ConnectBonusRoom()
        {
            CreateRoom(_currentCorridor, _data.BonusRoom);
        }

        private void ConnectStartRoom()
        {
            _currentRoom = Instantiate(_data.StartRoom, _roomContainer);
        }

        private void ConnectFinishRoom()
        {
            _currentRoom.HideExit();

            _currentRoom = CreateRoom(_currentCorridor, _data.TransitionRoom);

            _enterTriger = _currentRoom.gameObject.GetComponentInChildren<FinishLevelTriger>();
            _enterTriger.Construct(_data.NextStageId, _persistentDataService);

            _enterTriger.PlayerHasEntered += GenerateNextLevel;
        }

        private void GenerateNextLevel(PlayerHealth player)
        {
            _enterTriger.PlayerHasEntered -= GenerateNextLevel;
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