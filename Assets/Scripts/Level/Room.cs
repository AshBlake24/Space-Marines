using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Level
{
    public class Room : MonoBehaviour
    {
        private const int RotationModification = 180;
        [SerializeField] private List<ExitPoint> _exitPoints;
        [SerializeField] private Transform _miniMapIcon;
        [SerializeField] private int _spawnWeight;

        private ExitPoint _entryPoint;
        private List<ExitPoint> _doors = new List<ExitPoint>();
        private List<ExitPoint> _validExits = new List<ExitPoint>();
        [SerializeField] private int _levelMapPositionX;
        [SerializeField] private int _levelMapPositionY;

        public ExitPoint EntryPoint => _entryPoint;
        public int SpawnWeight => _spawnWeight;
        public int ExitCount => _exitPoints.Count;
        public int ValidExitCount => _validExits.Count;
        public int LevelMapPositionX => _levelMapPositionX;
        public int LevelMapPositionY => _levelMapPositionY;

        public void Init(ExitPoint connectingPoint, int PositionX, int PositionY)
        {
            if (connectingPoint != null)
            {
                _entryPoint = SelectEntryPoint();

                if (_entryPoint == null)
                    return;

                RotateRoom(connectingPoint);
            }

            _levelMapPositionX = PositionX;
            _levelMapPositionY = PositionY;

            FillValidExits();

            OpenDoor();
        }

        private void RotateRoom(ExitPoint connectingPoint)
        {
            Room previousRoom = connectingPoint.GetComponentInParent<Room>();

            float rotateAngle =
                -_entryPoint.Rotation
                + RotationModification
                + connectingPoint.Rotation
                + previousRoom.transform.rotation.eulerAngles.y;

            transform.rotation = Quaternion.Euler(0, rotateAngle, 0);

            if (_miniMapIcon != null)
                _miniMapIcon.rotation = Quaternion.Euler(90, -transform.rotation.y, 0);

            transform.position =
            Vector3.MoveTowards(transform.position, _entryPoint.transform.position, -GetShiftDistance());
        }

        public ExitPoint SelectExitPoint()
        {
            if (_validExits.Count <= 0)
                return null;

            ExitPoint connectingPoint = _validExits[Random.Range(0, _validExits.Count)];

            if (_validExits.Count != 0)
                _validExits.Remove(connectingPoint);

            return connectingPoint;
        }

        public void HideExit()
        {
            foreach (var exitPoint in _validExits)
            {
                exitPoint.Hide();
            }

            OpenDoor();
        }

        public void AddDoor(ExitPoint door)
        {
            _doors.Add(door);
        }

        public void CloseDoor()
        {
            foreach (var door in _doors)
            {
                door.Hide();
            }

            _entryPoint.Hide();
        }

        public void OpenDoor()
        {
            foreach (var door in _doors)
            {
                door.Show();
            }
            
            if (_entryPoint != null)
                _entryPoint.Show();
        }

        public float GetShiftDistance()
        {
            if (_entryPoint == null)
                return 0;

            return Vector3.Distance(transform.position, _entryPoint.transform.position);
        }

        public void FillValidExits()
        {
            foreach (var exit in _exitPoints)
            {
                _validExits.Add(exit);
            }
        }

        private ExitPoint SelectEntryPoint()
        {
            ExitPoint point;

            if (_exitPoints.Count != 0) 
                point = _exitPoints[Random.Range(0, _exitPoints.Count)];
            else
                point = null;

            _exitPoints.Remove(point);

            return point;
        }
    }
}