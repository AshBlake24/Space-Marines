using Roguelike.Enemies.Transitions;
using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class BossMeleeAttackState : EnemyState
    {
        [SerializeField] int _damageMultiplier;
        [SerializeField] float _attackRadius;
        [SerializeField] ParticleSystem _attackEffect;
        [SerializeField] TargetNotInRange _enemyZoneCheker;

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            if (_enemyZoneCheker.TryFinishState())
                return;
            else
                animator.PlayOptionalAttack();
        }

        public void Punch()
        {
            _attackEffect.Play();

            if (Vector3.Distance(enemy.Target.transform.position, transform.position) <= _attackRadius)
            {
                enemy.Target.TakeDamage(enemy.Damage * _damageMultiplier);
            }
        }

        public void TryFinishMeleeState()
        {
            _enemyZoneCheker.TryFinishState();
        }
    }
}