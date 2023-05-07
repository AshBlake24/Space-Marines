using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.StaticData.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Items
{
    public class EnemyItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<ItemId> _items;

        private IItemFactory _itemFactory;

        public void Construct(IItemFactory itemFactory, Enemy enemy)
        {
            _itemFactory = itemFactory;
            
            enemy.Health.Died += OnEnemyDied;
        }

        private void OnEnemyDied(EnemyHealth enemy)
        {
            Spawn(enemy.transform.position);
            
            enemy.Died -= OnEnemyDied;
        }

        private void Spawn(Vector3 spawnPosition) => 
            _itemFactory.CreateItem(spawnPosition, _items[Random.Range(0, _items.Count)]);
    }
}