using Roguelike.StaticData.Projectiles;

namespace Roguelike.Weapons.Projectiles.Stats
{
    public class ShrapnelStats : ProjectileStats
    {
        private readonly int _bulletsPerShot;

        public ShrapnelStats(ShrapnelStaticData staticData) : base(staticData)
        {
            _bulletsPerShot = staticData.BulletsPerShot;
        }
    }
}