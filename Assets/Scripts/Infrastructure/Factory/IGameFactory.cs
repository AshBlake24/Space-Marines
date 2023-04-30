using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Environment;
using Roguelike.Infrastructure.States;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Transform playerInitialPoint);
        GameObject CreateHud(EnvironmentType deviceType);
        GameObject GenerateLevel(GameStateMachine stateMachine);
        void CreateCharacterSelectionMode(GameStateMachine stateMachine);
        void Cleanup();
    }
}