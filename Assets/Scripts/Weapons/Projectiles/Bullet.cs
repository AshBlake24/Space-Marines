using System;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private BulletStats _stats;
        private ObjectPool<Projectile> _pool;
        private float _accumulatedTime;

        public override void Construct<TStats>(TStats stats, ObjectPool<Projectile> pool)
        {
            if (stats is BulletStats bulletStats)
                _stats = bulletStats;
            else
                throw new ArgumentNullException(nameof(stats), $"Expected to get the {typeof(BulletStats)}");

            if (pool != null)
                _pool = pool;
            else
                throw new ArgumentNullException(nameof(pool), "Pool cannot be null");
        }

        public override void Init()
        {
            _accumulatedTime = 0f;

            Rigidbody.velocity = Vector3.zero;
            Rigidbody.AddForce(transform.forward * _stats.Speed, ForceMode.VelocityChange);
        }

        private void Update()
        {
            LifetimeTick();
        }

        private void LifetimeTick()
        {
            _accumulatedTime += Time.deltaTime;

            if (_accumulatedTime >= _stats.Lifetime)
                _pool.AddInstance(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            _pool.AddInstance(this);
        }
    }
}