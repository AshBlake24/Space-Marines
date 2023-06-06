using Roguelike.UI.Windows.Enhancements;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    public class EnhancementViewerObserver : MonoBehaviour
    {
        [SerializeField] private EnhancementViewer _viewer;
        [SerializeField] private Button _sellButton;
        [SerializeField] private Image _sellButtonImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        private void OnEnable() => 
            _viewer.SellButtonInitialized += OnSellButtonInitialized;

        private void OnDisable() => 
            _viewer.SellButtonInitialized -= OnSellButtonInitialized;

        private void OnSellButtonInitialized()
        {
            _sellButtonImage.color = _sellButton.interactable 
                ? _activeColor 
                : _inactiveColor;
        }
    }
}