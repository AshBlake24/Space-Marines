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

        private Sequence _sequence;
        private Tween _landingTween;

        private void Start()
        {
            Vector3 defaultScale = transform.localScale;
            Vector3 jumpDistance = transform.forward * _jumpDistance;

            PlayAnimation(defaultScale, jumpDistance);
        }

        private void OnDisable()
        {
            _sequence.Kill();
            _landingTween.Kill();
        }

        private Vector3 CalculateGroundPosition(Vector3 position)
        {
            if (Physics.Raycast(position, Vector3.down, out RaycastHit hit))
            {
                Vector3 itemPosition = new(
                    position.x,
                    hit.point.y,
                    position.z);

                return itemPosition;
            }

            throw new ArgumentOutOfRangeException(nameof(transform.position), "Ground is out of reach");
        }

        private void PlayAnimation(Vector3 defaultScale, Vector3 jumpDistance)
        {
            _sequence = DOTween.Sequence();

            _sequence.SetUpdate(false);
            _sequence.Append(transform
                .DOScale(defaultScale, _scalingDuration)
                .From(Vector3.zero));

            if (CheckGroundOn(transform.position + jumpDistance))
            {
                _sequence.Insert(0, transform
                    .DOJump(transform.position + jumpDistance, _jumpPower, 1, _jumpDuration)
                    .SetEase(Ease.InQuad));
            }
            else
            {
                _sequence.Insert(0, transform
                    .DOJump(transform.position, _jumpPower, 1, _jumpDuration)
                    .SetEase(Ease.InQuad));
            }

            _sequence.onComplete = LandObjectToGround;
        }

        private void LandObjectToGround()
        {
            Vector3 position = CalculateGroundPosition(transform.position);

            _landingTween = transform.DOMove(position, _landingDuration)
                .SetEase(Ease.OutQuad);

            _landingTween.onComplete = () => enabled = false;
        }

        private bool CheckGroundOn(Vector3 position) => Physics.Raycast(position, Vector3.down);
    }
}