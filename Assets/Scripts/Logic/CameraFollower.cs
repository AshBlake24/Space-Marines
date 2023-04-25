using System;
using UnityEngine;

namespace Roguelike.Logic
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private Vector3 _rotationOffset;

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
    }
}