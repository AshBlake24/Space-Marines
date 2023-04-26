using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Environment;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Transform playerInitialPoint);
        GameObject CreateHud(EnvironmentType deviceType);
        GameObject GenerateLevel();
        void Cleanup();
    }
}