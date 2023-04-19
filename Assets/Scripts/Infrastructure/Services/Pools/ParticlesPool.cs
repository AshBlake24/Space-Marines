using Roguelike.Utilities;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Infrastructure.Services.Pools
{
    public class ParticlesPool
    {
        private readonly ObjectPool<ParticleSystem> _pool;
        private readonly ParticleSystem _particlesPrefab;
        private readonly Transform _container;

        public ParticlesPool(ParticleSystem particlesPrefab, int defaultSize, int maxSize)
        {
            _container = new GameObject($"Pool - {particlesPrefab.gameObject.name}").transform;
            _container.SetParent(Helpers.GetGeneralPoolsContainer());
            _particlesPrefab = particlesPrefab;
            _pool = new ObjectPool<ParticleSystem>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false,
                defaultSize,
                maxSize);
        }

        public ParticleSystem Get() => 
            _pool.Get();

        public void Release(ParticleSystem particles) => 
            _pool.Release(particles);

        private ParticleSystem CreatePoolItem()
        {
            ParticleSystem particles = Object.Instantiate(_particlesPrefab).GetComponent<ParticleSystem>();
            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            ReturnToPool returnToPool = particles.gameObject.AddComponent<ReturnToPool>();
            returnToPool.SetPool(_pool, particles);
            
            particles.transform.SetParent(_container);

            return particles;
        }

        private void OnTakeFromPool(ParticleSystem particles) => 
            particles.gameObject.SetActive(true);

        private void OnReleaseToPool(ParticleSystem particles) => 
            particles.gameObject.SetActive(false);

        private void OnDestroyItem(ParticleSystem particles) => 
            Object.Destroy(particles.gameObject);
    }
}