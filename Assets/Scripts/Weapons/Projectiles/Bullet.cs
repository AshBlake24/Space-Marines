using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private BulletStats _stats;

        public override ProjectileStats Stats => _stats;

        public void Construct(BulletStats stats)
        {
            _stats = stats;
        }
    }
}