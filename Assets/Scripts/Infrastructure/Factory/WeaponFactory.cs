using System;
using Roguelike.Infrastructure.Services.PersistentData;
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
        private readonly IPersistentDataService _progressData;

        public WeaponFactory(IStaticDataService staticDataService, ISaveLoadService saveLoadService,
            IPersistentDataService progressData)
        {
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _progressData = progressData;
        }

        public IWeapon CreateWeapon(WeaponId id) // todo refactor fabric
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

        public void CreateInteractableWeapon(WeaponId weaponId, Transform at)
        {
            // todo player dropped weapon
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
                : Object.Instantiate(weaponData.WeaponPrefab, parent.position, Quaternion.identity, parent)
                    .GetComponent<RangedWeapon>();

            weapon.Construct(InitializeRangedWeaponStats(weaponData));
            weapon.ReadProgress(_progressData.PlayerProgress);
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