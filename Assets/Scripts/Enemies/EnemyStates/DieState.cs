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

            DisableStateMachine();
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

        private void DisableStateMachine()
        {
            EnemyStateMachine _stateMachine = GetComponent<EnemyStateMachine>();

            if (_stateMachine != null)
                _stateMachine.enabled = false;
        }
    }
}