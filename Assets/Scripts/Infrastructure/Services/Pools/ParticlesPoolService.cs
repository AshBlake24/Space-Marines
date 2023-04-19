using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Pools
{
    public class ParticlesPoolService : IParticlesPoolService
    {
        private readonly Dictionary<string, ParticlesPool> _pools;

        public ParticlesPoolService()
        {
            _pools = new Dictionary<string, ParticlesPool>();
        }

        public ParticleSystem GetInstance(string key) =>
            _pools.TryGetValue(key, out ParticlesPool pool)
                ? pool.Get()
                : null;

        public void ReleaseInstance(string key, ParticleSystem particles)
        {
            if (_pools.ContainsKey(key))
                _pools[key].Release(particles);
        }

        public void CreateNewPool(string key, ParticleSystem particlesPrefab, int defaultSize = 10, int maxSize = 100)
        {
            if (_pools.ContainsKey(key) == false)
            {
                ParticlesPool pool = new ParticlesPool(particlesPrefab, defaultSize, maxSize);
                _pools.Add(key, pool);
            }
        }
    }
}