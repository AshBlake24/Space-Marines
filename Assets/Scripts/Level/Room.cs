using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Level
{
    public class Room : MonoBehaviour
    {
        [SerializeField] private List<ExitPoint> _exitPoints;
        [SerializeField] private GameObject _entryPoint;
        [SerializeField] private Transform _miniMapIcon;

        private List<ExitPoint> _doors = new List<ExitPoint>();
        private ExitPoint _entryDoor;

        public void Init(ExitPoint connectingPoint)
        {
            if (_entryPoint == null)
                return;

            transform.rotation = Quaternion.Euler(0, transform.rotation.y + connectingPoint.Rotation + _entryPoint.transform.eulerAngles.y, 0);

            if (_miniMapIcon != null)
                _miniMapIcon.rotation = Quaternion.Euler(90, -transform.rotation.y, 0);

            transform.position =
            Vector3.MoveTowards(transform.position, _entryPoint.transform.position, -GetShiftDistance());
            _entryPoint.TryGetComponent<ExitPoint>(out _entryDoor);
        }

        public ExitPoint SelectExitPoint()
        {
            ExitPoint connectingPoint = _exitPoints[Random.Range(0, _exitPoints.Count)];

            while (connectingPoint.IsNextZoneFull(this) == true)
            {
                _exitPoints.Remove(connectingPoint);
                connectingPoint.Hide();

                if (_exitPoints.Count != 0)
                    connectingPoint = _exitPoints[Random.Range(0, _exitPoints.Count)];
                else
                    break;
            }

            if (connectingPoint != null)
                _doors.Add(connectingPoint);

            if (_exitPoints.Count != 0)
                _exitPoints.Remove(connectingPoint);

            return connectingPoint;
        }

        public void HideExit()
        {
            foreach (var exitPoint in _exitPoints)
            {
                exitPoint.Hide();
            }

            OpenDoor();
        }

        public void CloseDoor()
        {
            foreach (var door in _doors)
            {
                door.Hide();
            }

            _entryDoor.Hide();
        }

        public void OpenDoor()
        {
            foreach (var door in _doors)
            {
                door.Show();
            }
            
            if (_entryDoor != null)
                _entryDoor.Show();
        }

        public float GetShiftDistance()
        {
            return Vector3.Distance(transform.position, _entryPoint.transform.position);
        }
    }
}