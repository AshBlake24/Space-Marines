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
                return transform.localEulerAngles.y;
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
    }
}