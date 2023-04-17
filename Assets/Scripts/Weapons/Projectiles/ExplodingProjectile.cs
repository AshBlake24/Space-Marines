using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private ExplodingProjectileStats _stats;

        public override ProjectileStats Stats => _stats;
        public override void Init()
        {
            throw new System.NotImplementedException();
        }

        public void Construct(ExplodingProjectileStats stats)
        {
            _stats = stats;
        }
    }
}