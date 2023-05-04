using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        BaseWindow CreateWindow(IWindowService windowService, WindowId windowId);
        void CreateResurrectionWindow(IWindowService windowService, PlayerDeath playerDeath);
        void CreateUIRoot();
    }
}