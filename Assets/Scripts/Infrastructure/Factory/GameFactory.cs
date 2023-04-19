using Roguelike.Infrastructure.AssetManagement;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public GameObject CreatePlayer(Transform playerInitialPoint) => 
            _assetProvider.Instantiate(AssetPath.PlayerPath, playerInitialPoint.position);

        public GameObject GenerateLevel() =>
            _assetProvider.Instantiate(AssetPath.LevelGeneratorPath);
    }
}