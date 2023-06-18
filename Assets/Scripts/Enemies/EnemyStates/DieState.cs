using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            animator.PlayDie();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}