using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.Random;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyLootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField, Range(0f, 100f)] private float _powerupDropChance;

        private ILootFactory _lootFactory;
        private IRandomService _randomService;

        public void Construct(ILootFactory lootFactory, IRandomService randomService)
        {
            _lootFactory = lootFactory;
            _randomService = randomService;
        }

        private void Start() =>
            _health.OnDied += OnDied;

        private void OnDied(EnemyHealth enemy) =>
            TrySpawnLoot();

        private void TrySpawnLoot()
        {
            float roll = _randomService.Next(0f, 100f);

            if (roll <= _powerupDropChance)
                _lootFactory.CreateRandomPowerup(transform.position);
        }
    }
}