using Roguelike.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    public class MobileActionButton : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _fireIcon;
        [SerializeField] private Sprite _interactionIcon;
        
        private PlayerInteraction _playerInteraction;

        public void Construct(PlayerInteraction playerInteraction)
        {
            _playerInteraction = playerInteraction;
            _iconImage.sprite = _fireIcon;
            _playerInteraction.GotInteractable += OnGotInteractable;
            _playerInteraction.LostInteractable += OnLostInteractable;
        }

        private void OnDestroy()
        {
            _playerInteraction.GotInteractable -= OnGotInteractable;
            _playerInteraction.LostInteractable -= OnLostInteractable;
        }

        private void OnGotInteractable() => 
            _iconImage.sprite = _interactionIcon;

        private void OnLostInteractable() => 
            _iconImage.sprite = _fireIcon;
    }
}