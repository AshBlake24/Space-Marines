using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Windows;
using Roguelike.UI.Windows.Enhancements;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        BaseWindow CreateWindow(IWindowService windowService, WindowId windowId);
        GameObject CreateWeaponStatsViewer(IWindowService windowService, WeaponId weaponId);
        EnhancementShopWindow CreateEnhancementShop(IWindowService windowService, PlayerEnhancements playerEnhancements);
        void CreateResurrectionWindow(IWindowService windowService, PlayerDeath playerDeath);
        void CreateUIRoot();
    }
}