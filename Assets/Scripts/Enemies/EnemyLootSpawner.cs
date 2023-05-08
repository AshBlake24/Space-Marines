using System.Collections.Generic;
using Roguelike.Infrastructure.Factory;
using Roguelike.Powerups.Logic;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyLootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private List<PowerupEffect> _loot;
        
        private ILootFactory _lootFactory;

        public void Construct(ILootFactory lootFactory)
        {
            _lootFactory = lootFactory;
        }

        private void Start() => 
            _health.Died += OnDied;

        private void OnDied(EnemyHealth enemy) => 
            SpawnLoot();

        private void SpawnLoot() => 
            _lootFactory.CreatePowerup(_loot, transform.position);
    }
}