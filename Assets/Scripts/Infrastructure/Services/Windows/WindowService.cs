using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Player;
using Roguelike.UI.Windows;

namespace Roguelike.Infrastructure.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void OpenResurrectionWindow(PlayerDeath playerDeath) => 
            _uiFactory.CreateResurrectionWindow(this, playerDeath);

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