using Roguelike.Data;
using Roguelike.StaticData.Enhancements;
using Roguelike.UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Roguelike.UI.Windows.Enhancements
{
    public class PlayerEnhancementWidget : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private Image _icon;

        private EnhancementTooltip _tooltip;
        private string _name;
        private string _description;
        private string _currentTier;
        private string _currentValue;

        public void Construct(EnhancementTooltip tooltip, EnhancementStaticData enhancementData, 
            EnhancementData enhancementProgress)
        {
            _tooltip = tooltip;
            _name = enhancementData.Name.Value;
            _description = enhancementData.Description.Value;
            _icon.sprite = enhancementData.Icon;
            _currentTier = enhancementProgress.Tier.ToString();
            _currentValue = enhancementData.Tiers[enhancementProgress.Tier - 1].Value.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData) => Show();
        
        public void OnPointerDown(PointerEventData eventData) => Show();
        
        public void OnPointerExit(PointerEventData eventData) => Hide();
        
        private void OnDisable() => Hide();

        private void Show()
        {
            _tooltip.gameObject.SetActive(true);
            _tooltip.SetText(_description, _name, _currentTier, _currentValue);
        }

        private void Hide()
        {
            _tooltip.gameObject.SetActive(false);
        }
    }
}