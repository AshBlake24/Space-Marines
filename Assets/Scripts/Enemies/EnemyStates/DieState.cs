using Roguelike.Roguelike.Enemies.Animators;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

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
    }
}