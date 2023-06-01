using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        BaseWindow CreateWindow(IWindowService windowService, WindowId windowId);
        GameObject CreateWeaponStatsViewer(IWindowService windowService, WeaponId weaponId);
        void CreateResurrectionWindow(IWindowService windowService, PlayerDeath playerDeath);
        void CreateEnhancementShop(IWindowService windowService, PlayerEnhancements playerEnhancements);
        void CreateUIRoot();
    }
}