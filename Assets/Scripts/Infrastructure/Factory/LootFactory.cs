using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Loot;
using Roguelike.StaticData.Loot;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Infrastructure.Factory
{
    public class LootFactory : ILootFactory
    {
        private readonly IRandomService _randomService;
        private readonly IAssetProvider _assetProvider;
        private readonly ObjectPool<LootView> _lootViews;

        public LootFactory(IRandomService randomService, IAssetProvider assetProvider)
        {
            _randomService = randomService;
            _assetProvider = assetProvider;
            _lootViews = new ObjectPool<LootView>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false);
        }

        public void CreateLoot(IEnumerable<LootStaticData> loot, Vector3 position)
        {
            GameObject droppedItem = GetDroppedItem(loot);

            if (droppedItem != null)
            {
                LootView lootView = _lootViews.Get();
                lootView.transform.position = position;

                Object.Instantiate(
                    droppedItem,
                    lootView.Container);
            }
        }

        private GameObject GetDroppedItem(IEnumerable<LootStaticData> loot)
        {
            int randomNumber = _randomService.Next(1, 100);

            List<LootStaticData> possibleLoot = loot
                .Where(lootData => randomNumber <= lootData.DropChance)
                .ToList();

            return possibleLoot.Count > 0
                ? possibleLoot[_randomService.Next(0, possibleLoot.Count - 1)].LootPrefab
                : null;
        }

        private LootView CreatePoolItem()
        {
            LootView lootView = _assetProvider.Instantiate(AssetPath.LootPath)
                .GetComponent<LootView>();

            lootView.transform.SetParent(Helpers.GetPoolsContainer(lootView.name));

            return lootView;
        }

        private void OnTakeFromPool(LootView lootView) =>
            lootView.gameObject.SetActive(true);

        private void OnReleaseToPool(LootView lootView)
        {
            lootView.ClearContainer();
            lootView.gameObject.SetActive(false);
        }

        private void OnDestroyItem(LootView lootView) =>
            Object.Destroy(lootView.gameObject);
    }
}