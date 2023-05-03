using Roguelike.Infrastructure.Factory;
using Roguelike.Level;
using Roguelike.Player;
using Roguelike.StaticData.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyId> _enemies;
        [SerializeField] private List<Transform> _spawnPositions;
        [SerializeField] private EnterTriger _enterPoint;
        [SerializeField] private List<EnemyHealth> _enemiesInRoom;
        [SerializeField] private List<ExitPoint> _doors;
        [SerializeField] private Room _room;

        private PlayerHealth _player;
        private IEnemyFactory _enemyFactory;
        private float _encounter—omplexity;

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
            _encounter—omplexity = Random.Range(minEncounterComplexity, maxEncounerComplexity);
        }

        private void Spawn(Transform spawnPosition, PlayerHealth target)
        {
            GameObject enemy = _enemyFactory.CreateEnemy(spawnPosition, _enemies[Random.Range(0, _enemies.Count)], target);

            _encounter—omplexity -= enemy.GetComponentInChildren<EnemyStateMachine>().Enemy.Danger;

            _enemiesInRoom.Add(enemy.GetComponentInChildren<EnemyHealth>());
        }

        private void OnPlayerHasEntered(PlayerHealth player)
        {
            _player = player;

            foreach (var position in _spawnPositions)
            {
                Spawn(position.transform, player);

                if (_encounter—omplexity <= 0)
                    break;
            }

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
                if (_encounter—omplexity > 0)
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
    }
}