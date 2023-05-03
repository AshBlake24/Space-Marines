using System;
using Roguelike.Level;
using UnityEngine;

namespace Roguelike.Logic.Cameras
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private Transform _miniMapCamera;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private Wall _hiddenWall;
        private Vector3 _currentPosition;

        private void LateUpdate()
        {
            if(_following == null)
                return;
            
            transform.position = _following.position + _positionOffset;;
            transform.rotation = Quaternion.Euler(_rotationOffset);

            _currentPosition = _following.position;
            _currentPosition.y = _miniMapCamera.position.y;
            _miniMapCamera.position = _currentPosition;

            HideWall();
        }

        public void Follow(GameObject following)
        {
            if (following == null)
                throw new ArgumentNullException(nameof(following), "Following object can't be null!");
            
            _following = following.transform;
        }

        public void HideWall()
        {
            if (Physics.Raycast(transform.position, _following.position - transform.position, out RaycastHit hit, 20))
            {
                if (hit.collider.TryGetComponent<Wall>(out Wall wall))
                {
                    if (wall != _hiddenWall)
                    {
                        _hiddenWall?.Show();

                        wall.Hide();

                        _hiddenWall = wall;
                    }
                }
                else
                {
                    _hiddenWall?.Show();
                    _hiddenWall = null;
                }
            }
        }
    }
}