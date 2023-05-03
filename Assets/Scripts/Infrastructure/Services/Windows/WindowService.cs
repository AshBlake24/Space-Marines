using System;
using Roguelike.Infrastructure.Factory;
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

        public BaseWindow Open(WindowId windowId)
        {
            return windowId switch
            {
                WindowId.Unknown => throw new NotImplementedException(),
                WindowId.MainMenu => _uiFactory.CreateMainMenu(this, windowId),
                WindowId.ReturnHome => _uiFactory.CreateConfirmationWindow(this, windowId),
                WindowId.ReturnCharacterSelection => _uiFactory.CreateConfirmationWindow(this, windowId),
                _ => _uiFactory.CreateWindow(this, windowId)
            };
        }
    }
}