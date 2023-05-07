using Roguelike.Infrastructure.Factory;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyLootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        
        private ILootFactory _lootFactory;

        public void Construct(ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;
        }

        private void Start()
        {
            _health.Died += OnDied;
        }

        private void OnDied(EnemyHealth enemy) => 
            SpawnLoot();

        private void SpawnLoot()
        {
            GameObject loot = _lootFactory.CreateLoot();
            loot.transform.position = transform.position;
        }
    }
}