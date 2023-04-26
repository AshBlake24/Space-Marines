using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

namespace Roguelike.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private int _roomsCount;
        [SerializeField] private int _bonusRoomCount;
        [SerializeField] private Room _startRoom;
        [SerializeField] private List<Room> _corridors;
        [SerializeField] private List<Room> _areaRooms;
        [SerializeField] private List<Room> _bonusRooms;

        private IEnemyFactory _enemyFactory;
        private Room _currentRoom;
        private Room _currentCorridor;
        private ExitPoint _connectingPoint;

        public void BuildLevel(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;

            _currentRoom = Instantiate(_startRoom, _startPosition, Quaternion.identity);
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
            _currentCorridor = CreateRoom(_currentRoom, _corridors, _connectingPoint);
        }

        private void ConnectRoom()
        {
            Room areaRoom = CreateRoom(_currentCorridor, _areaRooms);

            areaRoom.gameObject.GetComponent<EnemySpawner>().Init(_enemyFactory);

            _currentRoom.HideExit();

            _currentRoom = areaRoom;
        }

        private void ConnectBonusRoom()
        {
            CreateRoom(_currentCorridor, _bonusRooms);
        }

        private Room CreateRoom(Room exitRoom, List<Room> roomType, ExitPoint currentExitPoint = null)
        {
            Room nextRoom = roomType[Random.Range(0, roomType.Count)];

            if (currentExitPoint == null)
                currentExitPoint = exitRoom.SelectExitPoint();

            nextRoom = Instantiate(nextRoom, currentExitPoint.transform.position, Quaternion.identity);

            nextRoom.Init(_connectingPoint);

            return nextRoom;
        }
    }
}