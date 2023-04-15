using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;
using Roguelike.Weapons.Stats;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class WeaponFactory : IWeaponFactory
    {
        private readonly IStaticDataService _staticDataService;

        public WeaponFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public IWeapon CreateWeapon(WeaponId id)
        {
            WeaponStaticData weaponData = _staticDataService.GetWeaponData(id);

            return ConstructWeapon(weaponData);
        }

        private IWeapon ConstructWeapon(WeaponStaticData weaponData)
        {
            return weaponData.WeaponType switch
            {
                WeaponType.Ranged => CreateRangedWeapon(weaponData as RangedWeaponStaticData),
                _ => throw new ArgumentNullException(nameof(WeaponType), "This weapon type does not exist")
            };
        }

        private IWeapon CreateRangedWeapon(RangedWeaponStaticData weaponData)
        {
            RangedWeapon weapon = Object.Instantiate(weaponData.Prefab).GetComponent<RangedWeapon>();
            weapon.Construct(InitializeRangedWeaponStats(weaponData));

            return weapon;
        }
        
        private RangedWeaponStats InitializeRangedWeaponStats(RangedWeaponStaticData weaponData) => 
            new RangedWeaponStats(weaponData);
    }
}