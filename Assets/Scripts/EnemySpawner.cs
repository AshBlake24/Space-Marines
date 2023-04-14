using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _spawnPositions;

    private void Start()
    {
        foreach (var position in _spawnPositions)
        {
            Spawn(position.transform);
        }
    }

    private void Spawn(Transform spawnPosition)
    {
        Instantiate(_enemies[Random.Range(0, _enemies.Count)], spawnPosition.position, Quaternion.identity);
    }
}
