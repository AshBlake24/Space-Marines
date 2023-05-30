
using Roguelike.Roguelike.Enemies.Animators;

namespace Roguelike.Enemies.EnemyStates
{
    public class ExplosionState : EnemyState
    {
        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            enemy.Target.TakeDamage(enemy.Damage);

            EnemyHealth health = GetComponentInChildren<EnemyHealth>();
            health.TakeDamage(health.MaxHealth);
        }
    }
}
