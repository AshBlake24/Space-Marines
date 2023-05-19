using UnityEngine;

namespace Roguelike.Level
{
    public class ExitPoint : MonoBehaviour
    {
        private const int CastDistance = 3;

        [SerializeField] private GameObject _openedDoor;
        [SerializeField] private GameObject _closedDoor;
        [SerializeField] private GameObject _castPoint;

        public float Rotation
        {
            get
            {
                float angle = transform.rotation.eulerAngles.y;

                if (transform.localPosition.x < 0 || transform.localPosition.z > 0)
                    return angle;
                else
                    return -angle;
            }
            private set { }
        }

        public void Hide()
        {
            if (_openedDoor != null && _closedDoor != null)
            {
                _openedDoor.SetActive(false);
                _closedDoor.SetActive(true);
            }
        }

        public void Show()
        {
            if (_openedDoor != null && _closedDoor != null)
            {
                _openedDoor.SetActive(true);
                _closedDoor.SetActive(false);
            }
        }

        public bool IsNextZoneFull(Room room)
        {
            if (_castPoint != null)
            {
                return Physics.Raycast(_castPoint.transform.position, _castPoint.transform.position + (transform.position - room.transform.position) * CastDistance, room.GetShiftDistance() * CastDistance);
            }

            return true;
        }
    }
}