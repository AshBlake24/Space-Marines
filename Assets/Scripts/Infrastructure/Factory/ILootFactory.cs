using Roguelike.Infrastructure.Services;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        void CreatePowerup(Vector3 position);
    }
}