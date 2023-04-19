using Roguelike;
using Roguelike.Level;
using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

namespace Roguelike.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyStateMachine> _enemies;
        [SerializeField] private List<GameObject> _spawnPositions;
        [SerializeField] private EnterTriger _enterPoint;
        [SerializeField] private List<EnemyStateMachine> _enemiesInRoom;
        [SerializeField] private List<ExitPoint> _doors;
        [SerializeField] private Room _room;

        private void OnEnable()
        {
            _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
        }

        private void OnDisable()
        {
            _enterPoint.PlayerHasEntered -= OnPlayerHasEntered;

            foreach (var enemy in _enemiesInRoom)
            {
                enemy.EnemyDied -= OnEnemyDied;
            }
        }

        private void Spawn(Transform spawnPosition, PlayerComponent target)
        {
            EnemyStateMachine enemy = Instantiate(_enemies[Random.Range(0, _enemies.Count)], spawnPosition.position, Quaternion.identity);

            _enemiesInRoom.Add(enemy);

            enemy.Init(target);
        }

        private void OnPlayerHasEntered(PlayerComponent player)
        {
            foreach (var position in _spawnPositions)
            {
                Spawn(position.transform, player);
            }

            foreach (var enemy in _enemiesInRoom)
            {
                enemy.EnemyDied += OnEnemyDied;
            }

            foreach (var door in _doors)
            {
                door.Hide();
            }
        }

        private void OnEnemyDied(EnemyStateMachine enemy)
        {
            enemy.EnemyDied -= OnEnemyDied;

            _enemiesInRoom.Remove(enemy);

            if (_enemiesInRoom.Count == 0)
            {
                foreach (var door in _doors)
                {
                    door.Show();
                    _room.HideExit();
                }
            }
        }
    }
}