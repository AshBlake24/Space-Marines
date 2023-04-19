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
        private string _projectileVFXKey;
        private string _impactVFXKey;
        private Transform _container;

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


            _projectileVFXKey = _stats.ProjectileVFX.gameObject.name;
            _impactVFXKey = _stats.ImpactVFX.gameObject.name;

            _particlesPool.CreateNewPool(_projectileVFXKey, _stats.ProjectileVFX);
            _particlesPool.CreateNewPool(_impactVFXKey, _stats.ImpactVFX);
        }

        public override void Init()
        {
            _accumulatedTime = 0f;

            Rigidbody.velocity = transform.forward * _stats.Speed;

            SpawnVFX(_projectileVFXKey, transform);
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

        private void SpawnVFX(string key, Transform parent = null)
        {
            ParticleSystem particles = _particlesPool.GetInstance(key);

            if (parent != null)
            {
                particles.transform.SetParent(transform);
                particles.transform.SetPositionAndRotation(parent.position, parent.rotation);
            }
            else
            {
                particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            }
            
            particles.gameObject.SetActive(true);
            particles.Play();
        }
    }
}