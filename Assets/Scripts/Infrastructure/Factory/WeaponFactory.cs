using System;
using System.Linq;
using Roguelike.Audio.Factory;
using Roguelike.Audio.Sounds;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Random;
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
        private readonly IProjectileFactory _projectileFactory;
        private readonly IRandomService _randomService;
        private readonly IAudioFactory _audioFactory;
        private readonly IPersistentDataService _persistentData;

        public WeaponFactory(IStaticDataService staticDataService, ISaveLoadService saveLoadService,
            IProjectileFactory projectileFactory, IRandomService randomService, IAudioFactory audioFactory, 
            IPersistentDataService persistentData)
        {
            _staticDataService = staticDataService;
            _saveLoadService = saveLoadService;
            _projectileFactory = projectileFactory;
            _randomService = randomService;
            _audioFactory = audioFactory;
            _persistentData = persistentData;
        }

        public IWeapon CreateWeapon(WeaponId id, Transform parent)
        {
            if (id == WeaponId.Unknown)
                return null;

            WeaponStaticData weaponData = _staticDataService.GetDataById<WeaponId, WeaponStaticData>(id);

            return ConstructWeapon(weaponData, parent);
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

            AmmoData ammoData = null;

            if (_persistentData.PlayerProgress != null)
                ammoData = _persistentData.PlayerProgress.PlayerWeapons.RangedWeaponsData
                    .SingleOrDefault(data => data.ID == weaponData.Id)?.AmmoData;

            weapon.Construct(InitializeRangedWeaponStats(weaponData), ammoData, _projectileFactory, _randomService);
            weapon.transform.localPosition = weapon.PositionOffset;
            weapon.transform.localRotation = Quaternion.Euler(weapon.RotationOffset);
            weapon.GetComponent<AudioPlayer>().Construct(_audioFactory, weaponData.Clip);
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