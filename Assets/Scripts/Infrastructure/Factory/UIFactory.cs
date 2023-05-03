using System;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData.Windows;
using Roguelike.UI.Elements;
using Roguelike.UI.Windows;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;
        private readonly IPersistentDataService _progressService;
        private readonly ISceneLoadingService _sceneLoadingService;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IPersistentDataService progressService, ISceneLoadingService sceneLoadingService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
            _sceneLoadingService = sceneLoadingService;
        }

        public BaseWindow CreateConfirmationWindow(IWindowService windowService, WindowId windowId)
        {
            BaseWindow window = CreateWindow(windowService, windowId);
            
            if (window is ReturnHomeWindow returnHome)
                returnHome.Construct(_staticData, _sceneLoadingService);
            else
                throw new ArgumentNullException(nameof(window), "The necessary component is missing");

            return window;
        }

        public BaseWindow CreateMainMenu(IWindowService windowService, WindowId windowId)
        {
            BaseWindow window = CreateWindow(windowService, windowId);

            if (window is MainMenu mainMenu)
                mainMenu.Construct(_staticData, _sceneLoadingService);
            else
                throw new ArgumentNullException(nameof(window), "The window has no MainWindow component");

            return window;
        }

        public BaseWindow CreateWindow(IWindowService windowService, WindowId windowId)
        {
            WindowConfig config = _staticData.GetWindowConfig(windowId);
            BaseWindow window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            window.Construct(_progressService);
            
            foreach (OpenWindowButton openWindowButton in window.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(windowService);

            return window;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}