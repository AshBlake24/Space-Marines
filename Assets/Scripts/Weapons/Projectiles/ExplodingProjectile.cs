using System;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private ExplodingProjectileStats _stats;

        protected override ProjectileStats Stats => _stats;

        public override void Construct<TStats>(TStats stats, IObjectPool<Projectile> projectilePool)
        {
            InitializeExplodingProjectileStats(stats);
            base.Construct(stats, projectilePool);
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnVFX(ImpactVFXKey);
            ReturnToPool();
        }
        
        private void InitializeExplodingProjectileStats<TStats>(TStats stats)
        {
            if (stats is ExplodingProjectileStats explodingStats)
                _stats = explodingStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(ExplodingProjectileStats)}");
        }
    }
}