using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Logic;
using Roguelike.Loot.Powerups;
using Roguelike.StaticData.Loot;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IRandomService _randomService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IParticlesPoolService _particlesPoolService;
        private readonly IStaticDataService _staticData;
        private readonly IReadOnlyList<PowerupConfig> _powerupDropTable;
        private readonly int _powerupsTotalWeight;
        private readonly int _weaponsTotalWeight;

        public LootFactory(IRandomService randomService, IParticlesPoolService particlesPoolService,
            IStaticDataService staticData, ICoroutineRunner coroutineRunner)
        {
            _randomService = randomService;
            _coroutineRunner = coroutineRunner;
            _particlesPoolService = particlesPoolService;
            _staticData = staticData;
            _powerupDropTable = _staticData.PowerupDropTable.PowerupConfigs;
            _powerupsTotalWeight = _powerupDropTable.Sum(x => x.Weight);
            _weaponsTotalWeight = _staticData.WeaponsRarityWeights.Sum(x => (int)x.Value);
        }

        public void CreatePowerup(Vector3 position)
        {
            PowerupId droppedPowerup = GetDroppedPowerup();
            PowerupStaticData powerupData = _staticData.GetPowerupStaticData(droppedPowerup);

            Object.Instantiate(powerupData.Prefab, position, Quaternion.identity)
                .GetComponent<Powerup>()
                .Construct(_particlesPoolService, powerupData.Effect, powerupData.VFX);
            
            if (powerupData.Effect is ILastingEffect lastingEffect)
                lastingEffect.Construct(_coroutineRunner);
        }
        
        private PowerupId GetDroppedPowerup()
        {
            int roll = _randomService.Next(0, _powerupsTotalWeight);

            foreach (PowerupConfig powerup in _powerupDropTable)
            {
                roll -= powerup.Weight;

                if (roll < 0)
                    return powerup.Id;
            }

            throw new ArgumentOutOfRangeException(nameof(_powerupDropTable), "Incorrectly placed weights");
        }
        
        private WeaponId GetDroppedWeapon()
        {
            int roll = _randomService.Next(0, _weaponsTotalWeight);

            foreach ((WeaponId weaponId, RarityWeight rarity) in _staticData.WeaponsRarityWeights)
            {
                roll -= (int)rarity;

                if (roll < 0)
                    return weaponId;
            }

            throw new ArgumentOutOfRangeException(nameof(_staticData.WeaponsRarityWeights), "Incorrectly placed weights");
        }
    }
}