using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies.EnemyStates
{
    public class MeleeAttackState : EnemyState
    {
        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            animator.PlayAttack();

            enemy.Target.TakeDamage(enemy.Damage);
        }
    }
}