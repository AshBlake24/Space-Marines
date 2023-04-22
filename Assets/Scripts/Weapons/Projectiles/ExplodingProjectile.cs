using System;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    public class ExplodingProjectile : Projectile
    {
        private ExplodingProjectileStats _stats;
        private string _impactVFXKey;
        
        public override void Construct<TStats>(TStats stats, IObjectPool<Projectile> projectilePool)
        {
            base.Construct(stats, projectilePool);
            InitializeExplodingProjectileStats(stats);
            CreateImpactVFXPool();
            CreateProjectileVFX();
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnVFX(_impactVFXKey);
            ReturnToPool();
        }

        public override void Init()
        {
            Rigidbody.velocity = transform.forward * _stats.Speed;
            AccumulatedTime = 0f;
            ProjectileVFX.Play();
        }

        protected override void LifetimeTick()
        {
            AccumulatedTime += Time.deltaTime;

            if (AccumulatedTime >= _stats.Lifetime)
                ReturnToPool();
        }
        
        protected override void CreateImpactVFXPool()
        {
            _impactVFXKey = _stats.ProjectileVFX.gameObject.name;
            ParticlesPool.CreateNewPool(_impactVFXKey, _stats.ImpactVFX);
        }

        protected override void CreateProjectileVFX()
        {
            ProjectileVFX = Instantiate(_stats.ProjectileVFX, transform.position, transform.rotation, transform);
            StopProjectileVFX();
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