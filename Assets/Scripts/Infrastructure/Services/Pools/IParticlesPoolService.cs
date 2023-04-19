using UnityEngine;

namespace Roguelike.Infrastructure.Services.Pools
{
    public interface IParticlesPoolService : IService
    {
        ParticleSystem GetInstance(string key);
        void ReleaseInstance(string key, ParticleSystem particles);
        void CreateNewPool(string key, ParticleSystem particlesPrefab, int defaultSize = 10, int maxSize = 100);
    }
}