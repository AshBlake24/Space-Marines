using Roguelike.Enemies.Transitions;
using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class MeleeAttackState : EnemyState
    {
        [SerializeField] float _attackRadius;
        [SerializeField] ParticleSystem _attackEffect;

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            animator.PlayAttack();
        }

        public void Punch()
        {
            _attackEffect.Play();

            if (Vector3.Distance(enemy.Target.transform.position, transform.position) <= _attackRadius)
            {
                enemy.Target.TakeDamage(enemy.Damage);
            }
        }
    }
}