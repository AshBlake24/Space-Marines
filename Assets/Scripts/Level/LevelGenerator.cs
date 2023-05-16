using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Loading;
using UnityEngine;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string ContainerName = "Environment";

        private IEnemyFactory _enemyFactory;
        private ISceneLoadingService _sceneLoadingService;
        private IPersistentDataService _persistentDataService;
        private LevelStaticData _data;
        private Transform _roomContainer;
        private EnterTriger _enterTriger;
        private ExitPoint _connectingPoint;
        private Room _currentCorridor;
        private Room _currentRoom;
        private int _roomsCount;
        private int _bonusRoomCount;

        public void Construct(LevelStaticData levelData, IPersistentDataService persistentDataService,
            ISceneLoadingService sceneLoadingService, IEnemyFactory enemyFactory)
        {
            _data = levelData;
            _roomsCount = levelData.ArenasCount;
            _bonusRoomCount = levelData.TreasureRoomsCount;
            _sceneLoadingService = sceneLoadingService;
            _persistentDataService = persistentDataService;
            _enemyFactory = enemyFactory;
        }

        public void BuildLevel()
        {
            _roomContainer = new GameObject(ContainerName).transform;

            ConnectStartRoom();

            while (_roomsCount > 0)
            {
                ConnectCorridor();

                ConnectRoom();

                if (_bonusRoomCount > 0)
                {
                    ConnectCorridor();
                    ConnectBonusRoom();
                    _bonusRoomCount--;
                }

                _roomsCount--;
            }

            ConnectCorridor();
            ConnectFinishRoom();
        }

        private void ConnectCorridor()
        {
            _connectingPoint = _currentRoom.SelectExitPoint();
            _currentCorridor = CreateRoom(_currentRoom, _data.Corridor, _connectingPoint);
        }

        private void ConnectRoom()
        {
            Room areaRoom = CreateRoom(_currentCorridor, _data.Arenas);

            if (areaRoom.gameObject.TryGetComponent<EnemySpawner>(out EnemySpawner enemySpawner))
                enemySpawner.Init(_enemyFactory,_data.MinEncounterComplexity, _data.MaxEncounterComplexity);

            _currentRoom.HideExit();

            _currentRoom = areaRoom;
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

            _enterTriger = _currentRoom.gameObject.GetComponentInChildren<EnterTriger>();
            _enterTriger.Construct(_data.NextStageId);

            _enterTriger.PlayerHasEntered += GenerateNextLevel;
        }

        private void GenerateNextLevel(PlayerHealth player)
        {
            _enterTriger.PlayerHasEntered -= GenerateNextLevel;
            _sceneLoadingService.Load(_persistentDataService.PlayerProgress.WorldData.CurrentLevel);
        }

        private Room CreateRoom(Room exitRoom, List<Room> roomType, ExitPoint currentExitPoint = null)
        {
            Room nextRoom = roomType[Random.Range(0, roomType.Count)];

            if (currentExitPoint == null)
                currentExitPoint = exitRoom.SelectExitPoint();

            nextRoom = Instantiate(nextRoom, currentExitPoint.transform.position, Quaternion.identity);

            nextRoom.Init(_connectingPoint);

            nextRoom.transform.SetParent(_roomContainer, true);

            return nextRoom;
        }
    }
}