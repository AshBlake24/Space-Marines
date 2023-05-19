using Roguelike.Data;
using Roguelike.Logic;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly int _damage;
        private readonly int _maxAmmo;
        private readonly int _bulletsPerShot;
        private readonly float _horizontalSpread;
        private readonly float _verticalSpread;
        private readonly float _projectileStartSpeed;
        private readonly ProjectileStaticData _projectileData;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _damage = weaponData.Damage;
            _maxAmmo = weaponData.MaxAmmo;
            _bulletsPerShot = weaponData.BulletsPerShot;
            _horizontalSpread = weaponData.HorizontalSpread;
            _verticalSpread = weaponData.VerticalSpread;
            _projectileStartSpeed = weaponData.ProjectileStartSpeed;
            _projectileData = weaponData.Projectile;
        }

        public int Damage => _damage;
        public int MaxAmmo => _maxAmmo;
        public ProjectileStaticData ProjectileData => _projectileData;
        public int BulletsPerShot => _bulletsPerShot;
        public float HorizontalSpread => _horizontalSpread;
        public float VerticalSpread => _verticalSpread;
        public float ProjectileStartSpeed => _projectileStartSpeed;
    }
}