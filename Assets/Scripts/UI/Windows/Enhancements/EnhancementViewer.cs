using System;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using Roguelike.StaticData.Enhancements;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Enhancements
{
    public class EnhancementViewer : MonoBehaviour
    {
        [SerializeField] private Button _sellButton;

        [Header("Info")] 
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _currentTier;
        [SerializeField] private TextMeshProUGUI _currentValue;
        [SerializeField] private TextMeshProUGUI _nextValue;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _price;
        [SerializeField] private string _noValueMark;

        private IPersistentDataService _progressService;
        private PlayerEnhancements _playerEnhancements;
        private EnhancementStaticData _enhancementData;
        private EnhancementData _enhancementProgress;

        public event Action SellButtonInitialized;

        private void OnDestroy()
        {
            _sellButton.onClick.RemoveAllListeners();
            _progressService.PlayerProgress.Balance.Changed -= OnBalanceChanged;
        }

        public void Construct(IPersistentDataService progressService, PlayerEnhancements playerEnhancements,
            EnhancementStaticData enhancementStaticData)
        {
            _progressService = progressService;
            _playerEnhancements = playerEnhancements;
            _enhancementData = enhancementStaticData;
            _progressService.PlayerProgress.Balance.Changed += OnBalanceChanged;

            InitViewer();
            OnBalanceChanged();
        }

        private void InitViewer()
        {
            InitEnhancementProgress();
            InitMainInfoFields();
            InitCurrentTierField();

            if (_enhancementProgress.Tier == 0)
                InitNewStatsFields();
            else
                InitCurrentStatsFields();
        }

        private void InitEnhancementProgress()
        {
            _enhancementProgress = _progressService.PlayerProgress.State.Enhancements
                .SingleOrDefault(item => item.Id == _enhancementData.Id);

            if (_enhancementProgress == null)
                _enhancementProgress = new EnhancementData(_enhancementData.Id, 0);
        }

        private void InitMainInfoFields()
        {
            _icon.sprite = _enhancementData.Icon;
            _name.text = _enhancementData.Name;
            _description.text = _enhancementData.Description;
        }

        private void InitCurrentTierField()
        {
            if (_enhancementProgress.Tier == _enhancementData.Tiers.Length)
                _currentTier.color = Color.red;

            _currentTier.text = $"{_enhancementProgress.Tier.ToString()}/{_enhancementData.Tiers.Length}";
        }

        private void InitNewStatsFields()
        {
            _price.text = _enhancementData.Tiers[_enhancementProgress.Tier].Price.ToString();
            _currentValue.text = _noValueMark;
            _nextValue.text = _enhancementProgress.Tier < _enhancementData.Tiers.Length
                ? _enhancementData.Tiers[_enhancementProgress.Tier + 1].Value.ToString()
                : _noValueMark;
        }

        private void InitCurrentStatsFields()
        {
            _price.text = _enhancementProgress.Tier < _enhancementData.Tiers.Length
                ? _enhancementData.Tiers[_enhancementProgress.Tier].Price.ToString()
                : _noValueMark;

            _nextValue.text = _enhancementProgress.Tier < _enhancementData.Tiers.Length
                ? _enhancementData.Tiers[_enhancementProgress.Tier].Value.ToString()
                : _noValueMark;

            _currentValue.text = _enhancementData.Tiers[_enhancementProgress.Tier - 1].Value.ToString();
        }

        private void InitSellButton()
        {
            _sellButton.onClick.RemoveAllListeners();

            if (CurrentTierIsMax() || PlayerHasMoney() == false)
                _sellButton.interactable = false;
            else
                _sellButton.interactable = true;

            _sellButton.onClick.AddListener(OnSellButtonClick);
            SellButtonInitialized?.Invoke();
        }

        private void OnSellButtonClick()
        {
            _progressService.PlayerProgress.Statistics.EnhancementsBought++;
            _progressService.PlayerProgress.Balance.WithdrawCoins(_enhancementData.Tiers[_enhancementProgress.Tier].Price);

            if (_playerEnhancements.EnhancementExist(_enhancementData.Id) == false)
                _playerEnhancements.AddEnhancement(_enhancementData.Id, ++_enhancementProgress.Tier);
            else
                _playerEnhancements.Upgrade(_enhancementData.Id, ++_enhancementProgress.Tier);

            InitViewer();
            OnBalanceChanged();
        }

        private bool PlayerHasMoney() =>
            _progressService.PlayerProgress.Balance.Coins >= 
            _enhancementData.Tiers[_enhancementProgress.Tier].Price;

        private bool CurrentTierIsMax() =>
            _enhancementProgress.Tier >= _enhancementData.Tiers.Length;

        private void OnBalanceChanged() => InitSellButton();
    }
}