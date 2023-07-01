using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.Random;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyLootSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField, Range(0f, 100f)] private float _powerupDropChance = 25;

        private ILootFactory _lootFactory;
        private IRandomService _randomService;
        private bool _received;

        public void Construct(ILootFactory lootFactory, IRandomService randomService)
        {
            _lootFactory = lootFactory;
            _randomService = randomService;
            _received = false;
        }

        private void Start() =>
            _health.Died += OnDied;

        private void OnDied(EnemyHealth enemy) =>
            TrySpawnLoot();

        private void TrySpawnLoot()
        {
            if (_received == false)
            {
                float roll = _randomService.Next(0f, 100f);
            
                if (roll <= _powerupDropChance)
                    _lootFactory.CreateRandomPowerup(transform.position);

                _received = true;
            }
        }
    }
}