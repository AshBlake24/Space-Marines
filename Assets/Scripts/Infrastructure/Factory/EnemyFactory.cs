using Roguelike.Enemies;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ILootFactory _lootFactory;
        private readonly IRandomService _randomService;

        public EnemyFactory(IStaticDataService staticDataService, ILootFactory lootFactory,
            IRandomService randomService)
        {
            _staticDataService = staticDataService;
            _lootFactory = lootFactory;
            _randomService = randomService;
        }

        public GameObject CreateEnemy(Transform spawnPoint, EnemyId id, PlayerHealth target)
        {
            EnemyStaticData enemyData = _staticDataService.GetEnemyStaticData(id);
            GameObject enemyPrefab = Object.Instantiate(enemyData.Prefab, spawnPoint);

            Enemy enemy = new Enemy(enemyData, enemyPrefab.GetComponentInChildren<EnemyHealth>(), target);

            enemy.Health.Init(enemyData);

            enemyPrefab.GetComponent<EnemyStateMachine>().Init(enemy);
            enemyPrefab.GetComponentInChildren<EnemyLootSpawner>()
                .Construct(_lootFactory, _randomService);

            return enemyPrefab;
        }
    }
}