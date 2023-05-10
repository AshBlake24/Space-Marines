using DG.Tweening;
using UnityEngine;

namespace Roguelike.Animations
{
    public class PopUpItemAnimation : MonoBehaviour
    {
        [SerializeField] private float _scalingDuration;
        [SerializeField] private float _jumpDuration;
        [SerializeField] private float _jumpPower;
        [SerializeField] private float _jumpDistance;
        
        private void Start()
        {
            Vector3 defaultScale = transform.localScale;
            Vector3 jumpDistance = transform.forward * _jumpDistance;
            
            transform.DOScale(defaultScale, _scalingDuration)
                .From(Vector3.zero);

            transform.DOJump(transform.position + jumpDistance, _jumpPower, 1, _jumpDuration)
                .SetEase(Ease.InOutQuad)
                .onComplete = () => enabled = false;
        }
    }
}