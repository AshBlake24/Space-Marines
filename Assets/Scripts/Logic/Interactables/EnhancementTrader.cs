using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Enhancements;
using Roguelike.UI.Windows.Enhancements;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public class EnhancementTrader : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        
        private IWindowService _windowService;
        private HashSet<EnhancementStaticData> _soldEnhancements;

        public Outline Outline => _outline;
        public bool IsActive { get; private set; }
        
        private void Awake() => 
            _windowService = AllServices.Container.Single<IWindowService>();

        private void OnEnable()
        {
            Outline.enabled = false;
            IsActive = true;
        }
        
        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerEnhancements playerEnhancements))
            {
                EnhancementShopWindow enhancementShopWindow = _windowService.CreateEnhancementShop(playerEnhancements);

                if (_soldEnhancements == null)
                    _soldEnhancements = enhancementShopWindow.InitNewEnhancementViewers();
                else
                    enhancementShopWindow.InitEnhancementViewers(_soldEnhancements);
            }
        }
    }
}