using System;
using DG.Tweening;
using UnityEngine;

namespace Roguelike.Animations
{
    public class PopUpItemAnimation : MonoBehaviour
    {
        [SerializeField] private float _scalingDuration;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _landingDuration;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpDistance;
        
        private void Start()
        {
            Vector3 defaultScale = transform.localScale;
            Vector3 jumpDistance = transform.forward * _jumpDistance;
            
            PlayAnimation(defaultScale, jumpDistance);
        }

        private Vector3 CalculateGroundPosition(Vector3 currentPosition)
        {
            if (Physics.Raycast(currentPosition, Vector3.down, out RaycastHit hit))
            {
                Vector3 modelPosition = new(
                    currentPosition.x,
                    hit.point.y,
                    currentPosition.z);

                return modelPosition;
            }

            throw new ArgumentOutOfRangeException(nameof(transform.position), "Ground is out of reach");
        }

        private void PlayAnimation(Vector3 defaultScale, Vector3 jumpDistance)
        {
            transform.DOScale(defaultScale, _scalingDuration)
                .From(Vector3.zero);

            transform.DOJump(transform.position + jumpDistance, _jumpPower, 1, _jumpDuration)
                .SetEase(Ease.InQuad)
                .onComplete = LandObjectToGround;
        }

        private void LandObjectToGround()
        {
            Vector3 position = CalculateGroundPosition(transform.position);
            
            transform.DOMove(position, _landingDuration)
                .SetEase(Ease.OutQuad)
                .onComplete = () => enabled = false;
        }
    }
}