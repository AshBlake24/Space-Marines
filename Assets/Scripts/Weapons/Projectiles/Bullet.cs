using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private IParticlesPoolService _particlesPool;
        private BulletStats _stats;
        private Utilities.ObjectPool<Projectile> _bulletPool;
        private float _accumulatedTime;
        private string _impactVFXKey;
        private ParticleSystem _projectileVFX;

        private void Awake()
        {
            _particlesPool = AllServices.Container.Single<IParticlesPoolService>();
        }

        public override void Construct<TStats>(TStats stats, Utilities.ObjectPool<Projectile> bulletPool)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");

            if (bulletPool != null)
                _bulletPool = bulletPool;
            else
                throw new ArgumentNullException(nameof(bulletPool), "Pool cannot be null");


            _impactVFXKey = _stats.ProjectileVFX.gameObject.name;
            _particlesPool.CreateNewPool(_impactVFXKey, _stats.ImpactVFX);

            _projectileVFX = Instantiate(_stats.ProjectileVFX, transform.position, transform.rotation, transform);
            StopProjectileVFX();
        }

        public override void Init()
        {
            _accumulatedTime = 0f;

            Rigidbody.velocity = transform.forward * _stats.Speed;

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
            _bulletPool.AddInstance(this);
        }

        private void StopProjectileVFX() => 
            _projectileVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        private void SpawnVFX(string key)
        {
            ParticleSystem particles = _particlesPool.GetInstance(key);
            particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            particles.Play();
        }
    }
}