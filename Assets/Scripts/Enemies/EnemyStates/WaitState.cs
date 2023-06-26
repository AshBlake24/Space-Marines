
using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies.EnemyStates
{
    public class WaitState : EnemyState
    {
        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            animator.PlayIdle(true);
        }

        public override void Exit(EnemyState nextState)
        {
            animator.PlayIdle(false);

            base.Exit(nextState);
        }
    }
}