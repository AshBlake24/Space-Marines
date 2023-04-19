using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public event UnityAction EnemyDied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<PlayerComponent>(out PlayerComponent player))
        {
            EnemyDied?.Invoke();
            Destroy(gameObject);
        }
    }
}
