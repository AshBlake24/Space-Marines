using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {
        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            DisableNavMeshAgent();
            DisableColliders();

            animator.PlayDie();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }

        private void DisableColliders()
        {
            Collider[] _colliders = GetComponentsInChildren<Collider>();

            for (int i = 0; i < _colliders.Length; i++)
            {
                _colliders[i].enabled = false;
            }
        }

        private void DisableNavMeshAgent()
        {
            NavMeshAgent _agent = GetComponent<NavMeshAgent>();

            if (_agent != null)
                _agent.enabled = false;
        }
    }
}