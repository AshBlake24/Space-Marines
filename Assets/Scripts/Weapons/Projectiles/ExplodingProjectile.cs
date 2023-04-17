using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private ExplodingProjectileStats _stats;

        public override ProjectileStats Stats => _stats;

        public void Construct(ExplodingProjectileStats stats)
        {
            _stats = stats;
        }
    }
}