using System;
using Roguelike.Logic;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private readonly Collider[] _hits = new Collider[5];
        
        [SerializeField] private LayerMask _damageables;

        private ExplodingProjectileStats _stats;

        protected override ProjectileStats Stats => _stats;

        public override void Construct<TStats>(TStats projectileStats, IObjectPool<Projectile> projectilePool)
        {
            InitializeExplodingProjectileStats(projectileStats);
            base.Construct(projectileStats, projectilePool);
        }

        private void OnCollisionEnter(Collision collision)
        {
            for (int i = 0; i < Explode(); i++)
            {
                _hits[i].gameObject.GetComponentInChildren<IHealth>()
                    ?.TakeDamage(Damage);
            }

            OnImpacted();
            SpawnImpactVFX(ImpactVFXKey);
            ReturnToPool();
        }

        private int Explode() =>
            Physics.OverlapSphereNonAlloc(transform.position, _stats.ExplodingRadius, _hits, _damageables);

        private void InitializeExplodingProjectileStats<TStats>(TStats stats)
        {
            if (stats is ExplodingProjectileStats explodingStats)
                _stats = explodingStats;
            else
                throw new ArgumentNullException(nameof(stats),
                    $"Expected to get the {typeof(ExplodingProjectileStats)}");
        }
    }
}