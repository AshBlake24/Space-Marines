using System;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.StaticData;
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

        public BaseWindow Open(WindowId windowId) => 
            _uiFactory.CreateWindow(this, windowId);
    }
}