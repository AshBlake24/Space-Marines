using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Animations.UI
{
    public class GameCompleteWindowAnimations : MonoBehaviour
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
        [SerializeField] private NumberCounter _coinsCounter;
        [SerializeField] private NumberCounter _killsCounter;
        [SerializeField] private float _delayBetweenCounters;

        private GameObject[] _playerWeapons;
        private GameObject[] _playerEnhancements;
        private Sequence _stageSequence;
        private float _currentStage;
        private int _coins;
        private int _kills;

        public Transform WeaponsContent => _weaponsContent;
        public Transform EnhancementsContent => _enhancementsContent;

        public void Construct(GameObject[] weapons, GameObject[] enhancements)
        {
            _playerWeapons = weapons;
            _playerEnhancements = enhancements;

            _coinsCounter.NumberReached += OnNumberReached;
            _killsCounter.NumberReached += OnNumberReached;
        }

        private void OnDestroy()
        {
            _stageSequence.Kill();
            _coinsCounter.NumberReached -= OnNumberReached;
            _killsCounter.NumberReached -= OnNumberReached;
        }

        public void Init(int stage, int coins, int kills)
        {
            _currentStage = stage;
            _coins = coins;
            _kills = kills;
        }

        public void Play()
        {
            PlayStageViewerAnimation();
        }

        private void PlayStageViewerAnimation()
        {
            _stageSequence = DOTween.Sequence();

            _stageSequence.Append(_slider.DOValue(_currentStage, _slideDuration)
                .SetEase(_slideEase));

            _stageSequence.Append(_stagePointer
                .DOScale(Vector2.one * _punchSize, _punchDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(_punchEase));

            _stageSequence.OnComplete(PlayStatisticsAnimation);
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

        private void SetCoinsField() => _coinsCounter.UpdateText(_coins);

        private void SetKillsField() => _killsCounter.UpdateText(_kills);

        private void OnNumberReached(TextMeshProUGUI counter) =>
            counter.transform.DOScale(Vector2.one * _punchSize, _punchDuration)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(_punchEase);
    }
}