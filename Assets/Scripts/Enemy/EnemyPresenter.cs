using Roguelike.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Roguelike.Enemy
{
    public class EnemyPresenter : MonoBehaviour
    {
        [SerializeField] private EnemyStat _stat;

        public EnemyStat Stat => _stat;

        public event UnityAction EnemyDied;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerComponent player))
            {
                EnemyDied?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}