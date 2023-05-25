using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Levels.Spawner;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies
{
    [RequireComponent(typeof(Room))]
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private EnterTriger _enterPoint;
        [SerializeField] private float _spawnDuration;

        private SpawnerStaticData _data; 
        private float _encounterComplexity;
        private List<EnemyHealth> _enemiesInRoom;
        private Room _room;
        private List<SpawnPoint> _readySpawnPoints;
        private PlayerHealth _player;
        private IEnemyFactory _enemyFactory;

        private void OnEnable()
        {
            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnDisable()
        {
            _enterPoint.PlayerHasEntered -= OnPlayerHasEntered;

            foreach (var enemy in _enemiesInRoom)
            {
                enemy.Died -= OnEnemyDied;
            }
        }

        public void Init(IEnemyFactory enemyFactory, float minEncounterComplexity, float maxEncounerComplexity, SpawnerStaticData data)
        {
            _enemyFactory = enemyFactory;
            _encounterComplexity = data.Complexity * Random.Range(minEncounterComplexity, maxEncounerComplexity);

            _data = data;

            _readySpawnPoints = new List<SpawnPoint>();
            _enemiesInRoom = new List<EnemyHealth>();

            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                _readySpawnPoints.Add(_spawnPoints[i]);
            }

            _room = GetComponent<Room>();
        }

        private GameObject GenerateEnemy(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemyPrefab = _enemyFactory.CreateEnemy(spawnPosition, _data.Enemies[Random.Range(0, _data.Enemies.Count)], target);

            enemyPrefab.SetActive(false);

            _encounterComplexity -= enemyPrefab.GetComponentInChildren<EnemyStateMachine>().Enemy.Danger;

            return enemyPrefab;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            _player = player;

            while (_readySpawnPoints.Count > 0)
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

        private void OnEnemyDied(EnemyHealth enemy)
        {
            enemy.Died -= OnEnemyDied;

            _enemiesInRoom.Remove(enemy);

            if (_encounterComplexity > 0)
                Spawn();


            if (_enemiesInRoom.Count == 0)
                _room.OpenDoor();
        }

        private void OnEnemySpawned(SpawnPoint spawnpoint, GameObject enemyPrefab)
        {
            _readySpawnPoints.Add(spawnpoint);

            spawnpoint.EnemySpawned += OnEnemySpawned;

            EnemyHealth enemy = enemyPrefab.GetComponentInChildren<EnemyHealth>();

            _enemiesInRoom.Add(enemy);

            enemy.Died += OnEnemyDied;
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