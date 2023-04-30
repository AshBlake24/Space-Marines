using System;
using Roguelike.Infrastructure.AssetManagement;
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

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData,
            IPersistentDataService progressService)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
            _progressService = progressService;
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

        public GameObject CreateSelectionModeWindow()
        {
            GameObject instantiate = Object.Instantiate(Resources.Load(AssetPath.SelectionWindow), _uiRoot) as GameObject;

            return instantiate;
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}