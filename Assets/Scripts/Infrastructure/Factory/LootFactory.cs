using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Powerups.Data;
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

        public void CreatePowerup(IEnumerable<PowerupEffect> loot, Vector3 position)
        {
            PowerupEffect droppedItem = GetDroppedItem(loot);
            
            if (droppedItem != null)
                Object.Instantiate(droppedItem.Prefab, position, Quaternion.identity);
        }

        private PowerupEffect GetDroppedItem(IEnumerable<PowerupEffect> loot)
        {
            int randomNumber = _randomService.Next(1, 100);
            
            List<PowerupEffect> possibleLoot = loot
                .Where(item => randomNumber <= item.DropChance)
                .ToList();

            return possibleLoot.Count > 0
                ? possibleLoot[_randomService.Next(0, possibleLoot.Count - 1)]
                : null;
        }
    }
}