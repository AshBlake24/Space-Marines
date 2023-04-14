using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private int _roomsCount;
    [SerializeField] private int _bonusRoomCount;
    [SerializeField] private Room _startRoom;
    [SerializeField] private List<Room> _corridors;
    [SerializeField] private List<Room> _areaRooms;
    [SerializeField] private List<Room> _bonusRooms;

    private Room _currentRoom;
    private Room _currentCorridor;
    private ExitPoint _connectingPoint;

    private void Start()
    {
        _currentRoom = Instantiate(_startRoom, _startPosition, Quaternion.identity);
        _roomsCount--;

        BuildLevel();
    }

    private void BuildLevel()
    {
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
    }

    private void ConnectCorridor()
    {
        _connectingPoint = _currentRoom.SelectExitPoint();
        _currentCorridor = CreateRoom(_currentRoom, _corridors, _connectingPoint);
    }

    private void ConnectRoom()
    {
        Room areaRoom = CreateRoom(_currentCorridor, _areaRooms);

        _currentRoom.HideExit();

        _currentRoom = areaRoom;
    }

    private void ConnectBonusRoom()
    {
        Room bonusRoom = CreateRoom(_currentCorridor, _bonusRooms);
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
