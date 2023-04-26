using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Items;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IItemFactory : IService
    {
        public GameObject CreateItem(Vector3 spawnPoint, ItemId id);
    }
}
