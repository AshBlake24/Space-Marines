using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.StaticData.Loot;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IRandomService _randomService;
        private readonly IAssetProvider _assetProvider;

        public LootFactory(IRandomService randomService, IAssetProvider assetProvider)
        {
            _randomService = randomService;
            _assetProvider = assetProvider;
        }

        public void CreateLoot(IEnumerable<LootStaticData> loot, Vector3 position)
        {
            LootStaticData droppedItem = GetDroppedItem(loot);
            
            if (droppedItem != null)
                Object.Instantiate(droppedItem.LootPrefab, position, Quaternion.identity);
        }

        private LootStaticData GetDroppedItem(IEnumerable<LootStaticData> loot)
        {
            int randomNumber = _randomService.Next(1, 100);
            
            List<LootStaticData> possibleLoot = loot
                .Where(lootData => randomNumber <= lootData.DropChance)
                .ToList();

            return possibleLoot.Count > 0
                ? possibleLoot[_randomService.Next(0, possibleLoot.Count - 1)]
                : null;
        }
    }
}