using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Weapons.Stats
{
    public class RangedWeaponStats : WeaponStats
    {
        private readonly int _maxAmmo;
        private readonly int _clipSize;
        private readonly float _reloadTime;
        private readonly bool _infinityAmmo;
        private readonly ProjectileStaticData _projectileData;

        public RangedWeaponStats(RangedWeaponStaticData weaponData) : base(weaponData)
        {
            _maxAmmo = weaponData.MaxAmmo;
            _clipSize = weaponData.ClipSize;
            _reloadTime = weaponData.ReloadTime;
            _infinityAmmo = weaponData.InfinityAmmo;
            _projectileData = weaponData.Projectile;
        }

        public int MaxAmmo => _maxAmmo;
        public int ClipSize => _clipSize;
        public float ReloadTime => _reloadTime;
        public bool InfinityAmmo => _infinityAmmo;
        public ProjectileStaticData ProjectileData => _projectileData;
    }
}