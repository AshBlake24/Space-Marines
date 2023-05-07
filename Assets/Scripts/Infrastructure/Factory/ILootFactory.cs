using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Loot;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        void CreateLoot(IEnumerable<LootStaticData> loot, Vector3 position);
    }
}