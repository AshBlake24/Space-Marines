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

        [Header("Info")] [SerializeField] private Image _icon;
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

        private void OnDestroy() => _sellButton.onClick.RemoveAllListeners();

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
            InitMainInfoFields(_enhancementData);
            InitCurrentTierField(_enhancementData, _enhancementProgress);

            if (_enhancementProgress.Tier == 0)
                InitNewStatsFields(_enhancementData, _enhancementProgress);
            else
                InitCurrentStatsFields(_enhancementData, _enhancementProgress);
        }

        private void InitEnhancementProgress()
        {
            _enhancementProgress = _progressService.PlayerProgress.State.Enhancements
                .SingleOrDefault(item => item.Id == _enhancementData.Id);

            if (_enhancementProgress == null)
                _enhancementProgress = new EnhancementData(_enhancementData.Id, 0);
        }

        private void InitMainInfoFields(EnhancementStaticData enhancementStaticData)
        {
            _icon.sprite = enhancementStaticData.Icon;
            _name.text = enhancementStaticData.Name;
            _description.text = enhancementStaticData.Description;
        }

        private void InitCurrentTierField(EnhancementStaticData enhancementStaticData,
            EnhancementData enhancementProgress)
        {
            if (enhancementProgress.Tier == enhancementStaticData.Tiers.Length)
                _currentTier.color = Color.red;

            _currentTier.text = $"{enhancementProgress.Tier.ToString()}/{enhancementStaticData.Tiers.Length}";
        }

        private void InitNewStatsFields(EnhancementStaticData enhancementStaticData,
            EnhancementData enhancementProgress)
        {
            _price.text = enhancementStaticData.Tiers[enhancementProgress.Tier].Price.ToString();
            _currentValue.text = _noValueMark;
            _nextValue.text = enhancementProgress.Tier < enhancementStaticData.Tiers.Length
                ? enhancementStaticData.Tiers[enhancementProgress.Tier + 1].Value.ToString()
                : _noValueMark;
        }

        private void InitCurrentStatsFields(EnhancementStaticData enhancementStaticData,
            EnhancementData enhancementProgress)
        {
            _price.text = enhancementStaticData.Tiers[enhancementProgress.Tier].Price.ToString();
            _currentValue.text = enhancementStaticData.Tiers[enhancementProgress.Tier].Value.ToString();
            _nextValue.text = enhancementProgress.Tier < enhancementStaticData.Tiers.Length
                ? enhancementStaticData.Tiers[enhancementProgress.Tier].Value.ToString()
                : _noValueMark;
        }

        private void InitSellButton(EnhancementStaticData enhancementStaticData, EnhancementData enhancementProgress)
        {
            _sellButton.onClick.RemoveAllListeners();

            if (CurrentTierIsMax(enhancementStaticData, enhancementProgress) || PlayerHasMoney() == false)
                _sellButton.interactable = false;
            else
                _sellButton.interactable = true;

            _sellButton.onClick.AddListener(OnSellButtonClick);
        }

        private void OnSellButtonClick()
        {
            _progressService.PlayerProgress.Balance.Withdraw(_enhancementData.Tiers[_enhancementProgress.Tier].Price);

            if (_playerEnhancements.EnhancementExist(_enhancementData.Id) == false)
                _playerEnhancements.AddEnhancement(_enhancementData.Id, ++_enhancementProgress.Tier);
            else
                _playerEnhancements.Upgrade(_enhancementData.Id, ++_enhancementProgress.Tier);

            InitViewer();
        }

        private void OnBalanceChanged() =>
            InitSellButton(_enhancementData, _enhancementProgress);

        private bool PlayerHasMoney() =>
            _progressService.PlayerProgress.Balance.Coins >= _enhancementData.Tiers[_enhancementProgress.Tier].Price;

        private bool CurrentTierIsMax(EnhancementStaticData enhancementStaticData,
            EnhancementData enhancementProgress) =>
            enhancementProgress.Tier == enhancementStaticData.Tiers.Length;
    }
}