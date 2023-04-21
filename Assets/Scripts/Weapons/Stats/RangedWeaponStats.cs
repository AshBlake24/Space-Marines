using Roguelike.Data;
using Roguelike.Logic;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly bool _infinityAmmo;
        private readonly int _maxAmmo;
        private readonly ProjectileStaticData _projectileData;
        private readonly int _bulletsPerShot;
        private readonly float _horizontalSpread;
        private readonly float _verticalSpread;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _maxAmmo = weaponData.MaxAmmo;
            _infinityAmmo = weaponData.InfinityAmmo;
            _projectileData = weaponData.Projectile;
            _bulletsPerShot = weaponData.BulletsPerShot;
            _horizontalSpread = weaponData.HorizontalSpread;
            _verticalSpread = weaponData.VerticalSpread;
        }

        public int MaxAmmo => _maxAmmo;
        public bool InfinityAmmo => _infinityAmmo;
        public ProjectileStaticData ProjectileData => _projectileData;
        public int BulletsPerShot => _bulletsPerShot;
        public float HorizontalSpread => _horizontalSpread;
        public float VerticalSpread => _verticalSpread;
    }
}