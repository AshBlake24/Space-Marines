using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.Powerups.Data;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        void CreatePowerup(IEnumerable<PowerupEffect> loot, Vector3 position);
    }
}