using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private IParticlesPoolService _particlesPool;
        private IObjectPool<Projectile> _bulletPool;
        private BulletStats _stats;
        private float _accumulatedTime;
        private string _impactVFXKey;
        private ParticleSystem _projectileVFX;

        private void Awake()
        {
            _particlesPool = AllServices.Container.Single<IParticlesPoolService>();
        }

        public override void Construct<TStats>(TStats stats, IObjectPool<Projectile> bulletPool)
        {
            InitializeBulletStats(stats);
            InitializeProjectilesPool(bulletPool);
            CreateImpactVFXPool();
            CreateProjectileVFX();
        }

        public override void Init()
        {
            Rigidbody.velocity = transform.forward * _stats.Speed;
            _accumulatedTime = 0f;
            _projectileVFX.Play();
        }

        private void Update()
        {
            LifetimeTick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnVFX(_impactVFXKey);
            ReturnToPool();
        }

        private void LifetimeTick()
        {
            _accumulatedTime += Time.deltaTime;

            if (_accumulatedTime >= _stats.Lifetime)
                ReturnToPool();
        }

        private void ReturnToPool()
        {
            Rigidbody.velocity = Vector3.zero;
            _bulletPool.Release(this);
        }

        private void SpawnVFX(string key)
        {
            ParticleSystem particles = _particlesPool.GetInstance(key);
            particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            particles.Play();
        }
        
        private void InitializeBulletStats<TStats>(TStats stats)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");
        }

        private void InitializeProjectilesPool(IObjectPool<Projectile> bulletPool)
        {
            if (bulletPool != null)
                _bulletPool = bulletPool;
            else
                throw new ArgumentNullException(nameof(bulletPool), "Projectiles pool cannot be null");
        }

        private void CreateImpactVFXPool()
        {
            _impactVFXKey = _stats.ProjectileVFX.gameObject.name;
            _particlesPool.CreateNewPool(_impactVFXKey, _stats.ImpactVFX);
        }

        private void CreateProjectileVFX()
        {
            _projectileVFX = Instantiate(_stats.ProjectileVFX, transform.position, transform.rotation, transform);
            StopProjectileVFX();
        }

        private void StopProjectileVFX() => 
            _projectileVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}