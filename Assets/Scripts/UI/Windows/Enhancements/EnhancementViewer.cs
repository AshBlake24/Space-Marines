using Roguelike.Data;
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

        private PlayerEnhancements _playerEnhancements;
        private EnhancementStaticData _enhancementData;
        private EnhancementData _enhancementProgress;

        private void OnDestroy() => _sellButton.onClick.RemoveAllListeners();

        public void Construct(EnhancementStaticData enhancementStaticData, EnhancementData enhancementProgress,
            PlayerEnhancements playerEnhancements)
        {
            _playerEnhancements = playerEnhancements;
            _enhancementData = enhancementStaticData;
            _enhancementProgress = enhancementProgress;

            InitMainInfoFields(enhancementStaticData);
            InitCurrentTierField(enhancementStaticData, enhancementProgress);

            if (enhancementProgress.Tier == 0)
                InitNewStatsFields(enhancementStaticData, enhancementProgress);
            else
                InitCurrentStatsFields(enhancementStaticData, enhancementProgress);

            InitSellButton(enhancementStaticData, enhancementProgress);
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
            _price.text = enhancementStaticData.Tiers[enhancementProgress.Tier - 1].Price.ToString();
            _currentValue.text = enhancementStaticData.Tiers[enhancementProgress.Tier - 1].Value.ToString();
            _nextValue.text = enhancementProgress.Tier < enhancementStaticData.Tiers.Length
                ? enhancementStaticData.Tiers[enhancementProgress.Tier].Value.ToString()
                : _noValueMark;
        }

        private void InitSellButton(EnhancementStaticData enhancementStaticData, EnhancementData enhancementProgress)
        {
            if (CurrentTierIsMax(enhancementStaticData, enhancementProgress) || PlayerHasMoney() == false)
                _sellButton.interactable = false;
            else
                _sellButton.interactable = true;

            _sellButton.onClick.AddListener(OnSellButtonClick);
        }

        private static bool CurrentTierIsMax(EnhancementStaticData enhancementStaticData,
            EnhancementData enhancementProgress) =>
            enhancementProgress.Tier == enhancementStaticData.Tiers.Length;

        private bool PlayerHasMoney() => true;

        private void OnSellButtonClick()
        {
            if (_playerEnhancements.EnhancementExist(_enhancementData.Id) == false)
                _playerEnhancements.AddEnhancement(_enhancementData.Id, ++_enhancementProgress.Tier);
            else
                _playerEnhancements.Upgrade(_enhancementData.Id, ++_enhancementProgress.Tier);

        }
    }
}