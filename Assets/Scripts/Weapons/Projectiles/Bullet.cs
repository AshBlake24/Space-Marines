using System;
using Roguelike.Logic;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private BulletStats _stats;
        private IHealth _cashedHealth;

        protected override ProjectileStats Stats => _stats;

        public override void Construct<TStats>(TStats projectileStats, IObjectPool<Projectile> bulletPool)
        {
            InitializeBulletStats(projectileStats);
            base.Construct(projectileStats, bulletPool);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IHealth>(out _cashedHealth))
                _cashedHealth.TakeDamage(Damage);

            OnImpacted();
            SpawnVFX(ImpactVFXKey);
            ReturnToPool();
        }
        
        private void InitializeBulletStats<TStats>(TStats stats)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");
        }
    }
}