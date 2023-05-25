using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Player;
using Roguelike.StaticData.Weapons;
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

        public BaseWindow CreateWindow(IWindowService windowService, WindowId windowId)
        {
            WindowConfig config = _staticData.GetDataById<WindowId, WindowConfig>(windowId);
            BaseWindow window = Object.Instantiate(config.WindowPrefab, _uiRoot);
            window.Construct(_progressService);
            
            foreach (OpenWindowButton openWindowButton in window.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(windowService);
            
            switch (window)
            {
                case MainMenu mainMenu:
                    mainMenu.Construct(_staticData, _sceneLoadingService);
                    break;
                case ConfirmationWindow confirmationWindow:
                    confirmationWindow.Construct(_staticData, _sceneLoadingService);
                    break;
                case GameOverWindow gameOverWindow:
                    gameOverWindow.Construct(_staticData);
                    break;
            }
            
            return window;
        }

        public GameObject CreateWeaponStatsViewer(WindowService windowService, WeaponId weaponId)
        {
            GameObject weaponStatsViewer = _assetProvider.Instantiate(AssetPath.WeaponStatsViewer, _uiRoot);
            WeaponStaticData weaponData = _staticData.GetDataById<WeaponId, WeaponStaticData>(weaponId);

            if (weaponData is RangedWeaponStaticData rangedWeaponData)
            {
                if (weaponStatsViewer.TryGetComponent(out RangedWeaponStatsViewer rangedWeaponStatsViewer))
                    rangedWeaponStatsViewer.Construct(_progressService, rangedWeaponData);
            }

            return weaponStatsViewer;
        }

        public void CreateResurrectionWindow(IWindowService windowService, PlayerDeath playerDeath)
        {
            BaseWindow window = CreateWindow(windowService, WindowId.Resurrection);
            
            if (window is ResurrectionWindow resurrectionWindow)
                resurrectionWindow.Construct(playerDeath);
        }

        public void CreateUIRoot() =>
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}