using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.States;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreatePlayer(Transform playerInitialPoint);
        GameObject CreateHud(GameObject player);
        GameObject GenerateLevel(GameStateMachine stateMachine);
        void CreateCharacterSelectionMode();
        void Cleanup();
    }
}