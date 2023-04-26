using UnityEngine;

namespace Roguelike.Level
{
    public class ExitPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _hideWall;
        [SerializeField] private GameObject _castPoint;

        public float Rotation
        {
            get
            {
                float angle = transform.rotation.eulerAngles.y;

                if (transform.localPosition.x > 0)
                    return Vector3.Angle(transform.localPosition, Vector3.forward) + angle;
                else
                    return -Vector3.Angle(transform.localPosition, Vector3.forward) + angle;
            }
            private set { }
        }

        public void Hide()
        {
            if (_hideWall != null)
                _hideWall.SetActive(true);
        }

        public void Show()
        {
            if (_hideWall != null)
                _hideWall.SetActive(false);
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