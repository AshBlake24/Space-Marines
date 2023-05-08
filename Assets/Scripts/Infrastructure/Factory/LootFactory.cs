using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Logic;
using Roguelike.Powerups.Logic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IRandomService _randomService;
        private readonly IAssetProvider _assetProvider;
        private readonly ICoroutineRunner _coroutineRunner;

        public LootFactory(IRandomService randomService, IAssetProvider assetProvider, ICoroutineRunner coroutineRunner)
        {
            _randomService = randomService;
            _assetProvider = assetProvider;
            _coroutineRunner = coroutineRunner;
        }

        public void CreatePowerup(IEnumerable<PowerupEffect> loot, Vector3 position)
        {
            PowerupEffect droppedItem = GetDroppedItem(loot);

            if (droppedItem != null)
            {
                GameObject item = Object.Instantiate(droppedItem.Prefab, position, Quaternion.identity);
                
                if (droppedItem is ILastingEffect lastingEffect)
                    lastingEffect.Construct(_coroutineRunner);
            }
                
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