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
        private ObjectPool<VFX> _trailVFXPool;
        private ObjectPool<VFX> _impactVFXPool;
        private ObjectPool<Projectile> _projectilePool;
        private VFX _trail;
        private VFX _impact;
        private float _accumulatedTime;

        public override void Construct<TStats>(TStats stats, ObjectPool<Projectile> projectilePool)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");

            if (projectilePool != null)
                _projectilePool = projectilePool;
            else
                throw new ArgumentNullException(nameof(projectilePool), "Pool cannot be null");

            _trailVFXPool = new ObjectPool<VFX>(ProjectileVFX.gameObject);
            _impactVFXPool = new ObjectPool<VFX>(ImpactVFX.gameObject);
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
            _projectilePool.AddInstance(this);
        }

        private void SpawnTrailVFX()
        {
            if (_trailVFXPool.HasObjects)
            {
                _trail = _trailVFXPool.GetInstance();
            }
            else
            {
                _trail = Instantiate(ProjectileVFX);
                _trail.Counstruct(_trailVFXPool);
            }

            _trail.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _trail.transform.SetParent(transform);
            _trail.gameObject.SetActive(true);
        }

        private void SpawnImpactVFX()
        {
            if (_impactVFXPool.HasObjects)
            {
                _impact = _impactVFXPool.GetInstance();
            }
            else
            {
                _impact = Instantiate(ImpactVFX);
                _impact.Counstruct(_impactVFXPool);
            }

            _impact.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _impact.gameObject.SetActive(true);
        }
    }
}