using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IAssetProvider _assetProvider;

        public LootFactory(IAssetProvider assetProvider, ISaveLoadService saveLoadService)
        {
            _assetProvider = assetProvider;
        }
        
        public GameObject CreateLoot() => 
            _assetProvider.InstantiateRegistered(AssetPath.Loot);
    }
}