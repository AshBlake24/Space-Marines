using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Loot.Chest;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.Tutorials;
using Roguelike.UI.Windows;
using Roguelike.UI.Windows.Enhancements;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        public event Action WindowOpened;
        public event Action WindowClosed;

        public void OpenResurrectionWindow(PlayerDeath playerDeath) => 
            _uiFactory.CreateResurrectionWindow(this, playerDeath);

        public void OpenWeaponChestWindow(SalableWeaponChest salableWeaponChest)
        {
            _uiFactory.CreateWeaponChestWindow(this, salableWeaponChest);
        }

        public GameObject OpenWeaponStatsViewer(WeaponId weaponId) => 
            _uiFactory.CreateWeaponStatsViewer(this, weaponId);

        public EnhancementShopWindow CreateEnhancementShop(PlayerEnhancements playerEnhancements)
        {
            EnhancementShopWindow enhancementShopWindow = _uiFactory.CreateEnhancementShop(this, playerEnhancements);
            SubscribeToWindow(enhancementShopWindow);

            return enhancementShopWindow;
        }

        public void OpenTutorial(TutorialId tutorialId) => 
            _uiFactory.CreateWindow(this, tutorialId, isTutorial: true);

        public BaseWindow Open(WindowId windowId)
        {
            BaseWindow window = windowId switch
            {
                WindowId.Unknown => throw new NotImplementedException(),
                _ => _uiFactory.CreateWindow(this, windowId, isTutorial: false)
            };

            SubscribeToWindow(window);

            return window;
        }

        private void SubscribeToWindow(BaseWindow window)
        {
            WindowOpened?.Invoke();
            window.Closed += OnWindowClosed;
        }

        private void OnWindowClosed(BaseWindow window)
        {
            window.Closed -= OnWindowClosed;
            WindowClosed?.Invoke();
        }
    }
}