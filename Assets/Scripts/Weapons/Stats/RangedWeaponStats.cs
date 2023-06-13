using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly int _maxAmmo;
        private readonly int _bulletsPerShot;
        private readonly float _horizontalSpread;
        private readonly float _verticalSpread;
        private readonly float _projectileStartSpeed;
        private readonly float _reloadingAmmoAmountMultiplier;
        private readonly ProjectileStaticData _projectileData;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _maxAmmo = weaponData.MaxAmmo;
            _bulletsPerShot = weaponData.BulletsPerShot;
            _horizontalSpread = weaponData.HorizontalSpread;
            _verticalSpread = weaponData.VerticalSpread;
            _projectileStartSpeed = weaponData.ProjectileStartSpeed;
            _projectileData = weaponData.Projectile;
            _reloadingAmmoAmountMultiplier = weaponData.ReloadingAmmoAmountMultiplier;
        }

        public int MaxAmmo => _maxAmmo;
        public ProjectileStaticData ProjectileData => _projectileData;
        public int BulletsPerShot => _bulletsPerShot;
        public float HorizontalSpread => _horizontalSpread;
        public float VerticalSpread => _verticalSpread;
        public float ProjectileStartSpeed => _projectileStartSpeed;
        public float ReloadingAmmoAmountMultiplier => _reloadingAmmoAmountMultiplier;
    }
}