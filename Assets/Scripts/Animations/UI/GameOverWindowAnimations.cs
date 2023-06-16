using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Animations.UI
{
    public class GameOverWindowAnimations : MonoBehaviour
    {
        [Header("Stage Viewer")] 
        [SerializeField] private Slider _slider;
        [SerializeField] private RectTransform _stagePointer;
        [SerializeField] private float _slideDuration = 2f;
        [SerializeField] private float _punchDuration = 0.4f;
        [SerializeField] private float _punchSize = 1.15f;
        [SerializeField] private Ease _slideEase = Ease.InCubic;
        [SerializeField] private Ease _punchEase = Ease.OutQuad;
        
        [Header("Equipment")]
        [SerializeField] private Transform _weaponsContent;
        [SerializeField] private Transform _enhancementsContent;
        [SerializeField] private float _popupDelay = 0.3f;
        [SerializeField] private float _popupDuration = 1f;

        [Header("Statistics")] 
        [SerializeField] private NumberCounter _coins;
        [SerializeField] private NumberCounter _kills;
        [SerializeField] private float _delayBetweenCounters;
        
        private float _currentStage;
        private GameObject[] _playerWeapons;
        private GameObject[] _playerEnhancements;

        public Transform WeaponsContent => _weaponsContent;
        public Transform EnhancementsContent => _enhancementsContent;

        public void Construct(GameObject[] weapons, GameObject[] enhancements)
        {
            _playerWeapons = weapons;
            _playerEnhancements = enhancements;

            _coins.NumberReached += OnNumberReached;
            _kills.NumberReached += OnNumberReached;
        }

        private void OnDestroy()
        {
            _coins.NumberReached -= OnNumberReached;
            _kills.NumberReached -= OnNumberReached;
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
            Sequence sequence = DOTween.Sequence();

            sequence.AppendCallback(SetEquipmentFields);
            sequence.AppendInterval(_delayBetweenCounters);
            sequence.AppendCallback(SetCoinsField);
            sequence.AppendInterval(_delayBetweenCounters);
            sequence.AppendCallback(SetKillsField);
        }

        private void SetEquipmentFields()
        {
            float delay = 0;

            for (int i = 0; i < _playerWeapons.Length; i++)
            {
                _playerWeapons[i].transform.DOScale(1f, _popupDuration)
                    .SetEase(Ease.OutBack)
                    .SetDelay(delay);

                delay += _popupDelay;
            }

            delay += _popupDelay;
            
            for (int i = 0; i < _playerEnhancements.Length; i++)
            {
                _playerEnhancements[i].transform.DOScale(1f, _popupDuration)
                    .SetEase(Ease.OutBack)
                    .SetDelay(delay);

                delay += _popupDelay;
            }
        }

        private void SetCoinsField() => _coins.UpdateText(100);

        private void SetKillsField() => _kills.UpdateText(666);

        private void OnNumberReached(TextMeshProUGUI counter) =>
            counter.transform.DOScale(Vector2.one * _punchSize, _punchDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(_punchEase);
    }
}