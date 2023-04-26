
namespace Roguelike.Enemies.EnemyStates
{
    public class MeleeAttackState : EnemyState
    {
        public override void Enter(Enemy enemy)
        {
            base.Enter(enemy);

            enemy.Target.TakeDamage(enemy.Damage);

            EnemyHealth health = GetComponentInChildren<EnemyHealth>();
            health.TakeDamage(health.MaxHealth);
        }
    }
}
