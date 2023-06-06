using System;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Windows;
using Roguelike.UI.Windows.Enhancements;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Windows
{
    public interface IWindowService : IService
    {
        public event Action WindowOpened;
        public event Action WindowClosed;
        
        BaseWindow Open(WindowId windowId);
        GameObject OpenWeaponStatsViewer(WeaponId weaponId);
        EnhancementShopWindow CreateEnhancementShop(PlayerEnhancements playerEnhancements);
        void OpenResurrectionWindow(PlayerDeath playerDeath);
    }
}