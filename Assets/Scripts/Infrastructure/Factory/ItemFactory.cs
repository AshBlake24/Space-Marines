using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Items;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class ItemFactory : IItemFactory
    {
        private readonly IStaticDataService _staticDataService;

        public ItemFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public GameObject CreateItem(Vector3 spawnPiont, ItemId id)
        {
            ItemStaticData itemData = _staticDataService.GetItemStaticData(id);
            GameObject enemyPrefab = Object.Instantiate(itemData.Prefab, spawnPiont, itemData.Prefab.transform.rotation);

            return enemyPrefab;
        }
    }
}
