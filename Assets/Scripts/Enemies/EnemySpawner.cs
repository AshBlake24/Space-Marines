using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Levels.Spawner;
using System.Collections.Generic;
using Roguelike.Data;
using Roguelike.Logic.Popups;
using UnityEngine;

namespace Roguelike.Enemies
{
    [RequireComponent(typeof(Room))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private float _spawnDuration;

        private float _encounterComplexity;
        private EnterTriger _enterPoint;
        private SpawnerStaticData _data;
        private List<EnemyHealth> _enemiesInRoom;
        private Room _room;
        private List<SpawnPoint> _readySpawnPoints;
        private PlayerHealth _player;
        private PlayerProgress _playerProgress;
        private DamagePopupViewer _damagePopupViewer;
        private IEnemyFactory _enemyFactory;

        private void OnDisable()
        {
            _enterPoint.PlayerHasEntered -= OnPlayerHasEntered;

            foreach (var enemy in _enemiesInRoom)
            {
                enemy.Died -= OnEnemyDied;
            }
        }

        public void Init(IEnemyFactory enemyFactory, PlayerProgress playerProgress,
            float minEncounterComplexity, float maxEncounerComplexity, SpawnerStaticData data)
        {
            _enemyFactory = enemyFactory;
            _playerProgress = playerProgress;
            _encounterComplexity = data.Complexity * Random.Range(minEncounterComplexity, maxEncounerComplexity);

            _data = data;

            _readySpawnPoints = new List<SpawnPoint>();
            _enemiesInRoom = new List<EnemyHealth>();

            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                _readySpawnPoints.Add(_spawnPoints[i]);
            }

            _room = GetComponent<Room>();

            _enterPoint = _room.EntryPoint.GetComponent<EnterTriger>();
            _damagePopupViewer = GetComponentInChildren<DamagePopupViewer>();

            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private GameObject GenerateEnemy(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemyPrefab = _enemyFactory.CreateEnemy(spawnPosition,
                _data.Enemies[Random.Range(0, _data.Enemies.Count)], target);

            enemyPrefab.SetActive(false);

            _encounterComplexity -= enemyPrefab.GetComponentInChildren<EnemyStateMachine>().Enemy.Danger;

            EnemyHealth enemy = enemyPrefab.GetComponentInChildren<EnemyHealth>();

            _damagePopupViewer.SubscribeToEnemy(enemy);
            _enemiesInRoom.Add(enemy);

            enemy.Died += OnEnemyDied;

            return enemyPrefab;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            _player = player;

            int spawnPointsCount = _readySpawnPoints.Count;

            if (_data.MinSpawnPointsInWave < _readySpawnPoints.Count)
                spawnPointsCount = Random.Range(_data.MinSpawnPointsInWave, _readySpawnPoints.Count + 1);

            for (int i = 0; i < spawnPointsCount; i++)
            {
                Spawn();

                if (_encounterComplexity <= 0)
                {
                    _readySpawnPoints.Clear();
                    break;
                }
            }

            _room.CloseDoor();
        }

        private void OnEnemyDied(EnemyHealth enemyHealth)
        {
            enemyHealth.Died -= OnEnemyDied;

            _enemiesInRoom.Remove(enemyHealth);

            if (_encounterComplexity > 0)
                Spawn();

            if (_enemiesInRoom.Count == 0)
                _room.OpenDoor();

            _playerProgress.Statistics.KillData.CurrentKillData.OnMonsterKilled();
            _playerProgress.Balance.AddCoins(enemyHealth.GetComponentInParent<EnemyStateMachine>().Enemy.Coins);
        }

        private void OnEnemySpawned(SpawnPoint spawnpoint, GameObject enemyPrefab)
        {
            _readySpawnPoints.Add(spawnpoint);

            spawnpoint.EnemySpawned -= OnEnemySpawned;
        }

        private SpawnPoint GetRandomSpawnPoint()
        {
            SpawnPoint point = _readySpawnPoints[Random.Range(0, _readySpawnPoints.Count)];

            _readySpawnPoints.Remove(point);

            return point;
        }

        private void Spawn()
        {
            SpawnPoint spawnPoint = GetRandomSpawnPoint();

            GameObject enemy = GenerateEnemy(spawnPoint.transform, _player);

            spawnPoint.Spawn(enemy, _spawnDuration);

            spawnPoint.EnemySpawned += OnEnemySpawned;
        }
    }
}