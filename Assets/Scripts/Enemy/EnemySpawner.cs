using Roguelike;
using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AI;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyStateMachine> _enemies;
    [SerializeField] private List<GameObject> _spawnPositions;
    [SerializeField] private EnterTriger _enterPoint;

    private void OnEnable()
    {
        _enterPoint.PlayerHasEntered += OnPlayerHasEntered;
    }

    private void OnDisable()
    {
        _enterPoint.PlayerHasEntered -= OnPlayerHasEntered;
    }

    private void Spawn(Transform spawnPosition, PlayerComponent target)
    {
        EnemyStateMachine enemy = Instantiate(_enemies[Random.Range(0, _enemies.Count)], spawnPosition.position, Quaternion.identity);

        enemy.Init(target);
    }

    private void OnPlayerHasEntered(PlayerComponent player)
    {
        foreach (var position in _spawnPositions)
        {
            Spawn(position.transform, player);
        }
    }
}
