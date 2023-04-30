using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        BaseWindow CreateWindow(IWindowService windowService, WindowId windowId);
        GameObject CreateSelectionModeWindow();
        void CreateUIRoot();
    }
}