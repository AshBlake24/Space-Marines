using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly int _maxAmmo;
        private readonly int _clipSize;
        private readonly float _reloadTime;
        private readonly ProjectileStaticData _projectileData;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _projectileData = weaponData.Projectile;
            _maxAmmo = weaponData.MaxAmmo;
            _clipSize = weaponData.ClipSize;
            _reloadTime = weaponData.ReloadTime;
        }

        public int MaxAmmo => _maxAmmo;
        public int ClipSize => _clipSize;
        public float ReloadTime => _reloadTime;
        public ProjectileStaticData ProjectileData => _projectileData;
    }
}