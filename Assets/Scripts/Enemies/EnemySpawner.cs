using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyId> _enemies;
        [SerializeField] private List<SpawnPoint> _spawnPoints;
        [SerializeField] private EnterTriger _enterPoint;
        [SerializeField] private List<EnemyHealth> _enemiesInRoom;
        [SerializeField] private List<ExitPoint> _doors;
        [SerializeField] private float _spawnDuration;
        [SerializeField] private Room _room;

        [SerializeField] private List<SpawnPoint> _readySpawnPoints;
        private PlayerHealth _player;
        private IEnemyFactory _enemyFactory;
        private float _encounterComplexity;

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

        public void Init(IEnemyFactory enemyFactory, float minEncounterComplexity, float maxEncounerComplexity)
        {
            _enemyFactory = enemyFactory;
            _encounterComplexity = Random.Range(minEncounterComplexity, maxEncounerComplexity);
        }

        private GameObject GenerateEnemy(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemy = _enemyFactory.CreateEnemy(spawnPosition, _enemies[Random.Range(0, _enemies.Count)], target);

            _encounterComplexity -= enemy.GetComponentInChildren<EnemyStateMachine>().Enemy.Danger;

            _enemiesInRoom.Add(enemy.GetComponentInChildren<EnemyHealth>());

            return enemy;
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            _player = player;

            foreach (var point in _readySpawnPoints)
            {
                GameObject enemy = GenerateEnemy(point.transform, player);

                point.Spawn(enemy, _spawnDuration);

                point.SpawnReady += OnSpawnReady;

                if (_encounterComplexity <= 0)
                    break;
            }

            _readySpawnPoints.Clear();

            foreach (var enemy in _enemiesInRoom)
            {
                enemy.Died += OnEnemyDied;
            }

            foreach (var door in _doors)
            {
                door.Hide();
            }
        }

        private void OnEnemyDied(EnemyHealth enemy)
        {
            enemy.Died -= OnEnemyDied;

            _enemiesInRoom.Remove(enemy);

            if (_enemiesInRoom.Count == 0)
            {
                if (_encounterComplexity > 0)
                {
                    OnPlayerHasEntered(_player);
                    return;
                }

                foreach (var door in _doors)
                {
                    door.Show();
                    _room.HideExit();
                }
            }
        }

        private void OnSpawnReady(SpawnPoint spawnpoint)
        {
            _readySpawnPoints.Add(spawnpoint);
        }
    }
}