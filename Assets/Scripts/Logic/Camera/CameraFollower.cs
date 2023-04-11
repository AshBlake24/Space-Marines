using Roguelike.Level;
using System;
using UnityEngine;

namespace Roguelike.Logic.Camera
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

        private Wall _hiddenWall;

        private void LateUpdate()
        {
            if(_following == null)
                return;
            
            transform.position = _following.position + _positionOffset;;
            transform.rotation = Quaternion.Euler(_rotationOffset);
        }

        public void Follow(GameObject following)
        {
            if (following == null)
                throw new ArgumentNullException(nameof(following), "Following object can't be null!");
            
            _following = following.transform;
        }
        
        private Vector3 FollowingPointPosition()
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
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;
            return followingPosition;
                    _hiddenWall?.Show();
                    _hiddenWall = null;
                }
            }
        }
    }
}