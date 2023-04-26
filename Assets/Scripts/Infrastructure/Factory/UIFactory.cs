using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticData;

        private Transform _uiRoot;

        public UIFactory(IAssetProvider assetProvider, IStaticDataService staticData)
        {
            _assetProvider = assetProvider;
            _staticData = staticData;
        }

        public void CreatePauseMenu()
        {
            WindowConfig config = _staticData.GetWindowConfig(WindowId.PauseMenu);
            Object.Instantiate(config.WindowPrefab, _uiRoot);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}