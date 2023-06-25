using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Logic.Popups
{
    public class DamagePopup : MonoBehaviour
    {
        private const float MaxScale = 1.2f;
        private const float MinScale = 0.9f;
        private const float MaxHeightPosition = 0.5f;
        private const float MinHeightPosition = 0.2f;
        private const float PopupDurationMultiplicator = 0.1f;
        private const int MaxSortingOrder = 20;
        private const int MinSortingOrder = 0;

        [SerializeField] private TextMeshPro _text;
        [SerializeField] private float _lifetime;

        private static int s_sortingOrder;
        private IObjectPool<DamagePopup> _popupsPool;
        private Sequence _sequence;

        public void Construct(IObjectPool<DamagePopup> popupsPool)
        {
            _popupsPool = popupsPool;
        }

        private void OnDisable() => _sequence.Kill();

        public void SetValue(int value)
        {
            s_sortingOrder++;

            if (s_sortingOrder > MaxSortingOrder)
                s_sortingOrder = MinSortingOrder;
            
            _text.sortingOrder = s_sortingOrder++;
            
            
            _text.text = value.ToString();
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _sequence = DOTween.Sequence();

            float punchTime = _lifetime * PopupDurationMultiplicator;
            float height = Random.Range(MinHeightPosition, MaxHeightPosition);
            Vector3 endHeightPosition = transform.position + (Vector3.up * height);
            Vector3 scale = Vector3.one * Random.Range(MinScale, MaxScale);

            _sequence.Append(transform.DOScale(scale, punchTime)
                .From(Vector3.zero)
                .SetEase(Ease.OutQuad));
            
            _sequence.Append(transform.DOScale(Vector3.zero, _lifetime - punchTime)
                .SetEase(Ease.InQuad));
            
            _sequence.Insert(0, transform.DOLocalMove(endHeightPosition, _lifetime));
            _sequence.OnComplete(() => _popupsPool.Release(this));
        }
    }
}