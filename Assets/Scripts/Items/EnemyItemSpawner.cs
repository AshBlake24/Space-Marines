using Roguelike.Enemies;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Items
{
    public class EnemyItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<ItemId> _items;
            
        private Enemy _enemy;
        private IItemFactory _factory;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IItemFactory>();
        }

        public void Init(Enemy enemy)
        {
            _enemy= enemy;

            enemy.Health.Died += OnEnemyDied;
        }

        private void OnEnemyDied(EnemyHealth enemyHealth)
        {
            Spawn(enemyHealth.transform.position);

            enemyHealth.Died -= OnEnemyDied;
        }

        private void Spawn(Vector3 spawnPosition)
        {
            _factory.CreateItem(spawnPosition, _items[Random.Range(0, _items.Count)]);
        }
    }
}
