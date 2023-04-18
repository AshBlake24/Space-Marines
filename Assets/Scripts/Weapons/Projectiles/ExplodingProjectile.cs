using System;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private ExplodingProjectileStats _stats;

        public override void Construct<TStats>(TStats stats, ObjectPool<Projectile> pool)
        {
            throw new NotImplementedException();
        }

        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}