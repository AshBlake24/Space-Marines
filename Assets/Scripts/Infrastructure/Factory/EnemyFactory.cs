using Roguelike.Audio.Factory;
using Roguelike.Audio.Sounds;
using Roguelike.Enemies;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using Roguelike.UI.Elements;
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

        private EnemyStaticData _enemyData;
        private GameObject _enemyPrefab;
        private Enemy _enemy;

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
            EnemyConsctruct(spawnPoint, id, target);

            _enemy.Health.Init(_enemyData);

            return _enemyPrefab;
        }

        private void EnemyConsctruct(Transform spawnPoint, EnemyId id, PlayerHealth target)
        {
            _enemyData = _staticDataService.GetDataById<EnemyId, EnemyStaticData>(id);
            _enemyPrefab = Object.Instantiate(_enemyData.Prefab, spawnPoint);

            _enemy = new Enemy(_enemyData, _enemyPrefab.GetComponentInChildren<EnemyHealth>(), target);

            _enemyPrefab.GetComponent<EnemyStateMachine>().Init(_enemy);
            _enemyPrefab.GetComponentInChildren<EnemyLootSpawner>()
                .Construct(_lootFactory, _randomService);

            if (_enemyPrefab.TryGetComponent(out AudioPlayer audioPlayer))
                audioPlayer.Construct(_audioFactory, _enemyData.Sound);
        }

        public GameObject CreateEnemy(Transform spawnPoint, EnemyId id, PlayerHealth target, ActorUI bossUI)
        {
            EnemyConsctruct(spawnPoint, id, target);

            GameObject hud = Object.FindObjectOfType<ActorUI>().gameObject;

            Object.Instantiate(bossUI, hud.transform).Construct(_enemy.Health);

            _enemy.Health.Init(_enemyData);

            return _enemyPrefab;
        }
    }
}