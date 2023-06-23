using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            animator.PlayDie();
        }

        public virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}