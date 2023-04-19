using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons
{
    public class ReturnToPool : MonoBehaviour
    {
        private ObjectPool<ParticleSystem> _pool;
        private ParticleSystem _particleSystem;

        public void SetPool(ObjectPool<ParticleSystem> pool, ParticleSystem particles)
        {
            _pool = pool;
            _particleSystem = particles;
        }

        private void OnParticleSystemStopped()
        {
            _pool.Release(_particleSystem);
        }

        private void OnDisable()
        {
            _pool.Release(_particleSystem);
        }
    }
}