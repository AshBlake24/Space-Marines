using System;
using Roguelike.Logic;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private BulletStats _stats;
        private ObjectPool<ParticleSystem> _projectileVFXPool;
        private ObjectPool<ParticleSystem> _impactVFXPool;
        private ObjectPool<Projectile> _bulletPool;
        private ParticleSystem _projectileVFX;
        private ParticleSystem _impactVFX;
        private float _accumulatedTime;

        public override void Construct<TStats>(TStats stats, ObjectPool<Projectile> bulletPool)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");

            if (bulletPool != null)
                _bulletPool = bulletPool;
            else
                throw new ArgumentNullException(nameof(bulletPool), "Pool cannot be null");

            _projectileVFXPool = new ObjectPool<ParticleSystem>(_stats.ProjectileVFX.gameObject);
            _impactVFXPool = new ObjectPool<ParticleSystem>(_stats.ImpactVFX.gameObject);
        }

        public override void Init()
        {
            _accumulatedTime = 0f;

            Rigidbody.velocity = transform.forward * _stats.Speed;

            SpawnTrailVFX();
        }

        private void Update()
        {
            LifetimeTick();
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnImpactVFX();
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

        private void SpawnTrailVFX()
        {
            if (_projectileVFXPool.HasObjects)
            {
                _projectileVFX = _projectileVFXPool.GetInstance();
            }
            else
            {
                _projectileVFX = Instantiate(_stats.ProjectileVFX);
                //_trail.Counstruct(_projectileVFXPool);
            }

            _projectileVFX.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _projectileVFX.transform.SetParent(transform);
            _projectileVFX.gameObject.SetActive(true);
        }

        private void SpawnImpactVFX()
        {
            if (_impactVFXPool.HasObjects)
            {
                _impactVFX = _impactVFXPool.GetInstance();
            }
            else
            {
                _impactVFX = Instantiate(_stats.ImpactVFX);
                //_impact.Counstruct(_impactVFXPool);
            }

            _impactVFX.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _impactVFX.gameObject.SetActive(true);
        }
    }
}