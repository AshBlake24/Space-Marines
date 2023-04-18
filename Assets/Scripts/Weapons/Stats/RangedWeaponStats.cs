using Roguelike.Logic;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly int _maxAmmo;
        private readonly bool _infinityAmmo;
        private readonly ProjectileStaticData _projectileData;
        private readonly VFX _muzzleVFX;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _maxAmmo = weaponData.MaxAmmo;
            _infinityAmmo = weaponData.InfinityAmmo;
            _projectileData = weaponData.Projectile;
            _muzzleVFX = weaponData.MuzzleVFX;
        }

        public int MaxAmmo => _maxAmmo;
        public bool InfinityAmmo => _infinityAmmo;
        public ProjectileStaticData ProjectileData => _projectileData;
        public VFX MuzzleVFX => _muzzleVFX;
    }
}