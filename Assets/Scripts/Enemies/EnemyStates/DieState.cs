using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Enemies;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {
        private const EnemyId Mine = EnemyId.Mine;

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            if (enemy.Id == Mine)
                Die();
            else
                animator.PlayDie();
        }

        public void Die()
        {
            Destroy(gameObject);
        }
    }
}