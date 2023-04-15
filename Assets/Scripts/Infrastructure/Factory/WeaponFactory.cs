using System;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Weapons;
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

        public IWeapon CreateWeapon(WeaponId id)
        {
            WeaponStaticData weaponData = _staticDataService.GetWeaponData(id);

            return ConstructWeapon(weaponData);
        }
        
        public IWeapon CreateWeapon(WeaponId id, Transform parent)
        {
            WeaponStaticData weaponData = _staticDataService.GetWeaponData(id);

            return ConstructWeapon(weaponData, parent);
        }

        private IWeapon ConstructWeapon(WeaponStaticData weaponData, Transform parent = null)
        {
            return weaponData.WeaponType switch
            {
                WeaponType.Ranged => CreateRangedWeapon(weaponData as RangedWeaponStaticData, parent),
                _ => throw new ArgumentNullException(nameof(WeaponType), "This weapon type does not exist")
            };
        }

        private IWeapon CreateRangedWeapon(RangedWeaponStaticData weaponData, Transform parent)
        {
            RangedWeapon weapon = (parent == null) 
                ? Object.Instantiate(weaponData.Prefab).GetComponent<RangedWeapon>() 
                : Object.Instantiate(weaponData.Prefab, parent.position, Quaternion.identity, parent).GetComponent<RangedWeapon>();

            weapon.Construct(InitializeRangedWeaponStats(weaponData));
            weapon.transform.localPosition = weapon.PositionOffset;
            weapon.transform.localRotation = Quaternion.Euler(weapon.RotationOffset);
            weapon.gameObject.SetActive(false);
            
            _saveLoadService.RegisterProgressWatchers(weapon.gameObject);

            return weapon;
        }

        private RangedWeaponStats InitializeRangedWeaponStats(RangedWeaponStaticData weaponData) =>
            new RangedWeaponStats(weaponData);
    }
}