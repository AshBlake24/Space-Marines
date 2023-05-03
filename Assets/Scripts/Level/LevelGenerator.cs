using Roguelike.Data;
using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.States;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour, IProgressWriter
    {
        private const string ContainerName = "Environment";

        private int _roomsCount;
        private int _bonusRoomCount;
        private EnterTriger _enterTriger;
        private LevelStaticData _data;
        private Transform _roomContainer;
        private IEnemyFactory _enemyFactory;
        private Room _currentRoom;
        private Room _currentCorridor;
        private ExitPoint _connectingPoint;
        private GameStateMachine _stateMachine;
        private ISaveLoadService _saveLoadService;

        public void Init(LevelStaticData levelData, GameStateMachine stateMashine)
        {
            _data = levelData;
            _roomsCount = levelData.AreaRoomCount;
            _bonusRoomCount = levelData.BonusRoomCount;
            _stateMachine= stateMashine;

            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void BuildLevel(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
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
            Room areaRoom = CreateRoom(_currentCorridor, _data.AreaRooms);

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

            _currentRoom = CreateRoom(_currentCorridor, _data.FinishRoom);

            _enterTriger = _currentRoom.gameObject.GetComponentInChildren<EnterTriger>();

            _enterTriger.PlayerHasEntered += GenerateNextLevel;
        }

        private void GenerateNextLevel(PlayerHealth player)
        {
            _enterTriger.PlayerHasEntered -= GenerateNextLevel;

            _saveLoadService.SaveProgress();

            _stateMachine.Enter<LoadProgressState>();
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

        public void WriteProgress(PlayerProgress progress)
        {
            progress.WorldData.CurrentStage = _data.NextLevelId;
        }

        public void ReadProgress(PlayerProgress progress)
        {
        }
    }
}