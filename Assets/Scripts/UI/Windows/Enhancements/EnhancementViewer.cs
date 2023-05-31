using Roguelike.Data;
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
        
        public void Init(EnhancementStaticData enhancementStaticData, EnhancementData enhancementProgress)
        {
            _icon.sprite = enhancementStaticData.Icon;
            _name.text = enhancementStaticData.Name;
            _description.text = enhancementStaticData.Description;
            _currentTier.text = $"{enhancementProgress.Tier.ToString()}/{enhancementStaticData.Tiers.Length}";
            _price.text = enhancementStaticData.Tiers[enhancementProgress.Tier - 1].Price.ToString();
            _currentValue.text = enhancementStaticData.Tiers[enhancementProgress.Tier - 1].Value.ToString();
            _nextValue.text = enhancementProgress.Tier < enhancementStaticData.Tiers.Length 
                ? enhancementStaticData.Tiers[enhancementProgress.Tier].Value.ToString() 
                : _noValueMark;
        }
    }
}