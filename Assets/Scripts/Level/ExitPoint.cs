using UnityEngine;

namespace Roguelike.Level
{
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _openedDoor;
        [SerializeField] private GameObject _closedDoor;
        [SerializeField] private GameObject _castPoint;

        public float Rotation
        {
            get
            {
                float angle = transform.rotation.eulerAngles.y;

                if (transform.localPosition.x < 0)
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
                return Physics.Raycast(_castPoint.transform.position, _castPoint.transform.position + (transform.position - room.transform.position) * 3, room.GetShiftDistance() * 3);
            }

            return true;
        }
    }
}