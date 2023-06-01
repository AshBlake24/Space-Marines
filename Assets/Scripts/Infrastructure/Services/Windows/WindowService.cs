using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Windows;
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

        public GameObject OpenWeaponStatsViewer(WeaponId weaponId) => 
            _uiFactory.CreateWeaponStatsViewer(this, weaponId);

        public void OpenResurrectionWindow(PlayerDeath playerDeath) => 
            _uiFactory.CreateResurrectionWindow(this, playerDeath);

        public void CreateEnhancementShop(PlayerEnhancements playerEnhancements) => 
            _uiFactory.CreateEnhancementShop(this, playerEnhancements);

        public BaseWindow Open(WindowId windowId)
        {
            return windowId switch
            {
                WindowId.Unknown => throw new NotImplementedException(),
                _ => _uiFactory.CreateWindow(this, windowId)
            };
        }
    }
}