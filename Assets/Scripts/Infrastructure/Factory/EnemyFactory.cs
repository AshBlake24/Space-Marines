using Roguelike.Audio.Factory;
using Roguelike.Audio.Sounds;
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
        private readonly IAudioFactory _audioFactory;

        public EnemyFactory(IStaticDataService staticDataService, ILootFactory lootFactory,
            IRandomService randomService, IAudioFactory audioFactory)
        {
            _staticDataService = staticDataService;
            _lootFactory = lootFactory;
            _randomService = randomService;
            _audioFactory = audioFactory;
        }

        public GameObject CreateEnemy(Transform spawnPoint, EnemyId id, PlayerHealth target)
        {
            EnemyStaticData enemyData = _staticDataService.GetDataById<EnemyId, EnemyStaticData>(id);
            GameObject enemyPrefab = Object.Instantiate(enemyData.Prefab, spawnPoint);

            Enemy enemy = new Enemy(enemyData, enemyPrefab.GetComponentInChildren<EnemyHealth>(), target);

            enemy.Health.Init(enemyData);

            enemyPrefab.GetComponent<EnemyStateMachine>().Init(enemy);
            enemyPrefab.GetComponentInChildren<EnemyLootSpawner>()
                .Construct(_lootFactory, _randomService);
            
            if (enemyPrefab.TryGetComponent(out AudioPlayer audioPlayer))
                audioPlayer.Construct(_audioFactory, enemyData.Sound);

            return enemyPrefab;
        }
    }
}