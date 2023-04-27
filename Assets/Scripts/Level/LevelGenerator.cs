using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.StaticData.Levels;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private const string ContainerName = "Environment";

        private int _roomsCount;
        private int _bonusRoomCount;
        private LevelStaticData _data;
        private Transform _roomContainer;
        private IEnemyFactory _enemyFactory;
        private Room _currentRoom;
        private Room _currentCorridor;
        private ExitPoint _connectingPoint;

        public void Init(LevelStaticData leveData)
        {
            _data = leveData;
            _roomsCount = leveData.AreaRoomCount;
            _bonusRoomCount = leveData.BonusRoomCount;
        }

        public void BuildLevel(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _roomContainer = new GameObject(ContainerName).transform;

            ConnectStartRoom();

            _roomsCount--;

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

            _currentRoom.HideExit();

            NavMeshBuilder.BuildNavMesh();
        }

        private void ConnectCorridor()
        {
            _connectingPoint = _currentRoom.SelectExitPoint();
            _currentCorridor = CreateRoom(_currentRoom, _data.Corridor, _connectingPoint);
        }

        private void ConnectRoom()
        {
            Room areaRoom = CreateRoom(_currentCorridor, _data.AreaRooms);

            areaRoom.gameObject.GetComponent<EnemySpawner>().Init(_enemyFactory);

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