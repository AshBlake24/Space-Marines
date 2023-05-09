using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Logic;
using Roguelike.Loot.Powerups;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IRandomService _randomService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IParticlesPoolService _particlesPoolService;

        public LootFactory(IRandomService randomService, IParticlesPoolService particlesPoolService,
            ICoroutineRunner coroutineRunner)
        {
            _randomService = randomService;
            _coroutineRunner = coroutineRunner;
            _particlesPoolService = particlesPoolService;
        }

        public void CreatePowerup(IEnumerable<PowerupEffect> loot, Vector3 position)
        {
            PowerupEffect droppedItem = GetDroppedItem(loot);

            if (droppedItem != null)
            {
                Powerup powerup = Object.Instantiate(droppedItem.Prefab, position, Quaternion.identity)
                    .GetComponent<Powerup>();

                powerup.Construct(_particlesPoolService, droppedItem.VFX);

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