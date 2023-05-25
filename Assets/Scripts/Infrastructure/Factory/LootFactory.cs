using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Logic.Interactables;
using Roguelike.Loot.Powerups;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Weapons;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IRandomService _randomService;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IParticlesPoolService _particlesPoolService;
        private readonly IStaticDataService _staticData;
        private readonly IReadOnlyList<PowerupConfig> _powerupDropTable;
        private readonly int _powerupsTotalWeight;
        private readonly int _weaponsTotalWeight;

        public LootFactory(IAssetProvider assetProvider, IRandomService randomService,
            IParticlesPoolService particlesPoolService,
            IStaticDataService staticData, ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _randomService = randomService;
            _coroutineRunner = coroutineRunner;
            _particlesPoolService = particlesPoolService;
            _staticData = staticData;
            _powerupDropTable = _staticData.PowerupDropTable.PowerupConfigs;
            _powerupsTotalWeight = _powerupDropTable.Sum(x => x.Weight);
            _weaponsTotalWeight = _staticData.WeaponsDropWeights.Sum(x => x.Value);
        }

        public void CreateRandomPowerup(Vector3 position) => 
            CreatePowerup(GetDroppedPowerup(), position);

        public void CreateConcretePowerup(PowerupId powerupId, Vector3 position) => 
            CreatePowerup(powerupId, position);

        public GameObject CreateRandomWeapon(Vector3 position) => 
            CreateWeapon(GetDroppedWeapon(), position);

        public GameObject CreateConcreteWeapon(WeaponId weaponId, Vector3 position) => 
            CreateWeapon(weaponId, position);

        private void CreatePowerup(PowerupId powerupId, Vector3 position)
        {
            PowerupStaticData powerupData = _staticData.GetDataById<PowerupId, PowerupStaticData>(powerupId);

            Object.Instantiate(powerupData.Prefab, position, Quaternion.identity)
                .GetComponent<Powerup>()
                .Construct(_particlesPoolService, powerupData.Effect, powerupData.ActiveVFX);

            if (powerupData.Effect is ILastingEffect lastingEffect)
                lastingEffect.Construct(_coroutineRunner);
        }

        private GameObject CreateWeapon(WeaponId weaponId, Vector3 position)
        {
            WeaponStaticData weaponData = _staticData.GetDataById<WeaponId, WeaponStaticData>(weaponId);
            RarityStaticData rarityData = _staticData.GetDataById<RarityId, RarityStaticData>(weaponData.Rarity);

            InteractableWeapon interactableWeapon = _assetProvider
                .Instantiate(AssetPath.InteractableWeaponPath, position)
                .GetComponent<InteractableWeapon>();

            Object.Instantiate(rarityData.RingVFX, interactableWeapon.transform);
            Object.Instantiate(rarityData.GlowVFX, interactableWeapon.transform);
            GameObject model = Object.Instantiate(weaponData.InteractableWeaponPrefab, interactableWeapon.ModelContainer);
            
            interactableWeapon.Construct(weaponId, model.GetComponent<Outline>());
            
            return interactableWeapon.gameObject;
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
            
            foreach ((WeaponId weaponId, int weight) in _staticData.WeaponsDropWeights)
            {
                roll -= weight;

                if (roll < 0)
                    return weaponId;
            }

            throw new ArgumentOutOfRangeException(nameof(_staticData.WeaponsDropWeights), "Incorrectly placed weights");
        }
    }
}