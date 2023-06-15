using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Animations.UI
{
    public class GameOverWindowAnimations : MonoBehaviour
    {
        [Header("Stage Viewer")] 
        [SerializeField] private Slider _slider;
        [SerializeField] private float _slideDuration = 2f;
        [SerializeField] private float _delayBeforeSlide = 0.25f;
        [SerializeField] private Ease _slideEase = Ease.InCubic;
        [SerializeField] private RectTransform _stagePointer;
        [SerializeField] private float _punchSize = 1.15f;
        [SerializeField] private float _punchDuration = 0.4f;
        [SerializeField] private Ease _punchEase = Ease.OutQuad;
        
        [Header("Equipment")]
        [SerializeField] private Transform _weaponsContent;
        [SerializeField] private Transform _enhancementsContent;
        [SerializeField] private float _popupDelay = 0.3f;
        [SerializeField] private float _popupDuration = 1f;
        
        private float _currentStage;
        private GameObject[] _playerWeapons;
        private GameObject[] _playerEnhancements;

        public Transform WeaponsContent => _weaponsContent;
        public Transform EnhancementsContent => _enhancementsContent;

        public void Construct(GameObject[] weapons, GameObject[] enhancements)
        {
            _playerWeapons = weapons;
            _playerEnhancements = enhancements;
        }

        public void Init(int stage)
        {
            _currentStage = stage;
        }

        public void Play()
        {
            PlayStageViewerAnimation();
        }

        private void PlayStageViewerAnimation()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_slider.DOValue(_currentStage, _slideDuration)
                .SetEase(_slideEase));

            sequence.Append(_stagePointer
                .DOScale(Vector2.one * _punchSize, _punchDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(_punchEase));

            sequence.OnComplete(PlayStatisticsAnimation);
        }

        private void PlayStatisticsAnimation()
        {
            float delay = 0;

            for (int i = 0; i < _playerWeapons.Length; i++)
            {
                _playerWeapons[i].transform.DOScale(1f, _popupDuration)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(delay);

                delay += _popupDelay;
            }
            
            for (int i = 0; i < _playerEnhancements.Length; i++)
            {
                _playerEnhancements[i].transform.DOScale(1f, _popupDuration)
                    .SetEase(Ease.OutBounce)
                    .SetDelay(delay);

                delay += _popupDelay;
            }
        }
    }
}