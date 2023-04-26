using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
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
            var config = _staticData.GetWindowConfig(WindowId.PauseMenu);
        }

        public void CreateUIRoot() => 
            _uiRoot = _assetProvider.Instantiate(AssetPath.UIRootPath).transform;
    }
}