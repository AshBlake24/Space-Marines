using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Loading;
using UnityEngine;
using System.Linq;
using Roguelike.Ads;
using Roguelike.Assets.Scripts.Enemies;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const int LevelMapSizeWidth = 39;
        private const int LevelMapSizeHeight = 39;
        private const string ContainerName = "Rooms";

        private IAdsService _adsService;
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
        private Room[,] _levelMap;
        private int _arenaRoomsCount;
        private int _bonusRoomMaxCount;
        private int _totalWeightRoom;
        private int _currentMapPositionX;
        private int _currentMapPositionY;

        public void Construct(StageStaticData stageData, IPersistentDataService persistentDataService,
            ISceneLoadingService sceneLoadingService, IEnemyFactory enemyFactory, IAdsService adsService)
        {
            _data = stageData;
            _adsService = adsService;
            _sceneLoadingService = sceneLoadingService;
            _persistentDataService = persistentDataService;
            _enemyFactory = enemyFactory;

            _levelMap = new Room[LevelMapSizeHeight, LevelMapSizeWidth];

            GetRoomsCount();
        }

        public void BuildLevel()
        {
            _arenas = new List<Room>();

            if (_roomContainer == null)
                _roomContainer = new GameObject(ContainerName).transform;

            GetRoomsCount();
            GenerateRooms();

            foreach (var arena in _arenas)
            {
                arena.HideExit();
            }
        }

        private void GetRoomsCount()
        {
            _arenaRoomsCount = _data.ArenasCount;
            _bonusRoomMaxCount = _data.BonusRoomsMaxCount;
        }

        private void GenerateRooms()
        {
            ConnectStartRoom();

            if (_arenaRoomsCount > 0)
            {
                ConnectArenaRoom();
                GenerateArenas();
            }

            ConnectFinishRoom();
            GenerateBonusRoom();
        }

        private void GenerateBonusRoom()
        {
            while (_arenas.Count > 0)
            {
                if (_bonusRoomMaxCount > 0)
                {
                    _currentRoom = _arenas[Random.Range(0, _arenas.Count)];

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

        private bool TryConnectCorridor()
        {
            _connectingPoint = _currentRoom.SelectExitPoint();

            while (IsNextZoneFree(_connectingPoint,_currentRoom) == false)
            {
                _connectingPoint.Hide();

                if (_currentRoom.ValidExitCount > 0)
                    _connectingPoint = _currentRoom.SelectExitPoint();
                else
                    _currentRoom = GetRandomArena();

                if (_currentRoom == null)
                    return false;
            }

            _currentRoom.AddDoor(_connectingPoint);

            _currentCorridor = CreateRoom(_currentRoom, _data.Corridor, 0, 0, _connectingPoint);

            return true;
        }

        private Room GetRandomArena()
        {
            Room randomArena = _arenas[Random.Range(0, _arenas.Count)];

            while (randomArena.ValidExitCount <= 0)
            {
                _arenas.Remove(randomArena);

                if (_arenas.Count > 0)
                    randomArena = _arenas[Random.Range(0, _arenas.Count)];
                else
                    return null;
            }

            return randomArena;
        }

        private bool IsNextZoneFree(ExitPoint connectingPoint, Room room)
        {
            if (GetZPositionDifference(connectingPoint, room) == 0)
            {
                if (connectingPoint.transform.position.x > room.transform.position.x)
                    return IsLevelMapCellEmpty(1, 0);
                else
                    return IsLevelMapCellEmpty(-1, 0);
            }
            else
            {
                if (connectingPoint.transform.position.z > room.transform.position.z)
                    return IsLevelMapCellEmpty(0, 1);
                else
                    return IsLevelMapCellEmpty(0, -1);
            }
        }

        private float GetZPositionDifference(ExitPoint connectingPoint, Room room)
        {
            float differenceValue = connectingPoint.transform.position.z - room.transform.position.z;

            if (differenceValue > 0 && differenceValue < room.GetShiftDistance())
                differenceValue = 0;
            else if (differenceValue < 0 && differenceValue > -room.GetShiftDistance())
                differenceValue = 0;

            return differenceValue;
        }

        private bool IsLevelMapCellEmpty(int positionX, int PositionY)
        {
            _currentMapPositionX = _currentRoom.LevelMapPositionX + positionX;
            _currentMapPositionY = _currentRoom.LevelMapPositionY + PositionY;

            if (_levelMap[_currentMapPositionX, _currentMapPositionY] == null)
                return true;
            else
                return false;
        }

        private void ConnectArenaRoom()
        {
            if (TryConnectCorridor() == false)
                BuildLevel();

            Room arenaRoom = CreateRoom(_currentCorridor, _data.Arenas, _currentMapPositionX, _currentMapPositionY);

            if (arenaRoom.gameObject.TryGetComponent<EnemySpawner>(out EnemySpawner enemySpawner))
                enemySpawner.Init(_enemyFactory, _persistentDataService.PlayerProgress,
                    _data.MinComplexityMultiplication, _data.MaxComplexityMultiplication, _data.Spawner);

            _arenas.Add(arenaRoom);

            if (_currentRoom.ValidExitCount <= 0)
            {
                _currentRoom.HideExit();

                _currentRoom = arenaRoom;
            }

            _levelMap[arenaRoom.LevelMapPositionX, arenaRoom.LevelMapPositionY] = arenaRoom;

            _arenaRoomsCount--;
        }

        private void ConnectBonusRoom()
        {
            if (TryConnectCorridor() == false)
                return;

            _currentRoom.OpenDoor();

            Room bonusRoom = CreateRoom(_currentCorridor, _data.BonusRoom, _currentMapPositionX, _currentMapPositionY);

            _levelMap[bonusRoom.LevelMapPositionX, bonusRoom.LevelMapPositionY] = bonusRoom;

            _bonusRoomMaxCount--;
        }

        private void ConnectStartRoom()
        {
            _currentRoom = Instantiate(_data.StartRoom, _roomContainer);
            _currentRoom.Init(null, LevelMapSizeWidth / 2, LevelMapSizeHeight / 2);

            _levelMap[_currentRoom.LevelMapPositionX, _currentRoom.LevelMapPositionY] = _currentRoom;
        }

        private void ConnectFinishRoom()
        {
            if (TryConnectCorridor() == false)
                BuildLevel();

            _currentRoom.HideExit();

            _currentRoom = CreateRoom(_currentCorridor, _data.TransitionRoom, _currentMapPositionX, _currentMapPositionY);

            _finishRoom = _currentRoom.GetComponent<FinishRoom>();
            _finishRoom.SetNextLevel(_data.NextStageId, _persistentDataService);

            _finishRoom.PlayerFinishedLevel += TryShowAds;

            if (_currentRoom.TryGetComponent<BossSpawner>(out BossSpawner spawner))
            {
                spawner.Init(_enemyFactory, _persistentDataService);
                _currentRoom.OpenDoor();
            }

            _currentRoom.HideExit();
        }

        private void GenerateNextLevel()
        {
            _finishRoom.PlayerFinishedLevel -= TryShowAds;
            _persistentDataService.PlayerProgress.State.HasResurrected = false;
            _persistentDataService.PlayerProgress.Statistics.OnStageComplete(_data.Score);
            _sceneLoadingService.Load(_persistentDataService.PlayerProgress.WorldData.CurrentLevel);
        }

        private Room CreateRoom(Room exitRoom, List<Room> roomType, int MapPositionX, int MapPositionY, ExitPoint currentExitPoint = null)
        {
            Room nextRoom = GetRandomRoom(roomType);

            if (currentExitPoint == null)
                currentExitPoint = exitRoom.SelectExitPoint();

            nextRoom = Instantiate(nextRoom, currentExitPoint.transform.position, Quaternion.identity, _roomContainer);

            nextRoom.Init(currentExitPoint, MapPositionX, MapPositionY);

            return nextRoom;
        }

        private Room GetRandomRoom(List<Room> roomType)
        {
            _totalWeightRoom = roomType.Sum(x => x.SpawnWeight);

            int roll = Random.Range(0, _totalWeightRoom);

            foreach (Room room in roomType)
            {
                roll -= room.SpawnWeight;

                if (roll <= 0)
                    return room;
            }

            return null;
        }

        private void TryShowAds()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _adsService.ShowInterstitialAd();
#endif
            GenerateNextLevel();
        }
    }
}