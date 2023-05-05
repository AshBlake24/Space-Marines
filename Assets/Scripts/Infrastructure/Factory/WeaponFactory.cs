using System;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Logic.Interactables;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Weapons.PickupableWeapons;
using Roguelike.Weapons;
using Roguelike.Weapons.Stats;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISaveLoadService _saveLoadService;

        public WeaponFactory(IStaticDataService staticDataService, ISaveLoadService saveLoadService)
        {
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        public IWeapon CreateWeapon(WeaponId id) // todo refactor
        {
            if (id == WeaponId.Unknow)
                return null;
            
            WeaponStaticData weaponData = _staticDataService.GetWeaponData(id);

            return ConstructWeapon(weaponData);
        }
        
        public IWeapon CreateWeapon(WeaponId id, Transform parent)
        {
            if (id == WeaponId.Unknow)
                return null;
            
            WeaponStaticData weaponData = _staticDataService.GetWeaponData(id);

            return ConstructWeapon(weaponData, parent);
        }

        public void CreatePickupableWeapon(WeaponId weaponId, Transform at)
        {
            if (weaponId == WeaponId.Unknow)
                return;

            PickupableWeaponsConfig weaponConfig = _staticDataService.GetPickupableWeaponConfig(weaponId);
            Object.Instantiate(weaponConfig.Prefab, at.position, at.rotation);
        }

        private IWeapon ConstructWeapon(WeaponStaticData weaponData, Transform parent = null)
        {
            return weaponData.Type switch
            {
                WeaponType.Ranged => CreateRangedWeapon(weaponData as RangedWeaponStaticData, parent),
                _ => throw new ArgumentNullException(nameof(WeaponType), "This weapon type does not exist")
            };
        }

        private IWeapon CreateRangedWeapon(RangedWeaponStaticData weaponData, Transform parent)
        {
            RangedWeapon weapon = (parent == null) 
                ? Object.Instantiate(weaponData.WeaponPrefab).GetComponent<RangedWeapon>() 
                : Object.Instantiate(weaponData.WeaponPrefab, parent.position, Quaternion.identity, parent).GetComponent<RangedWeapon>();
            
            weapon.Construct(InitializeRangedWeaponStats(weaponData));
            weapon.transform.localPosition = weapon.PositionOffset;
            weapon.transform.localRotation = Quaternion.Euler(weapon.RotationOffset);
            weapon.Hide();
            
            RegisterWeapon(weapon.gameObject);

            return weapon;
        }

        private void RegisterWeapon(GameObject gameObject) => 
            _saveLoadService.RegisterProgressWatchers(gameObject);

        private RangedWeaponStats InitializeRangedWeaponStats(RangedWeaponStaticData weaponData) =>
            new RangedWeaponStats(weaponData);
    }
}