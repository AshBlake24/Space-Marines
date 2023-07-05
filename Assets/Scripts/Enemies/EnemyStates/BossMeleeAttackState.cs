using Roguelike.Enemies.Transitions;
using Roguelike.Logic;
using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class BossMeleeAttackState : EnemyState
    {
        private readonly Collider[] _hits = new Collider[1];

        [SerializeField] private LayerMask _explosionMask;
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

            for (int i = 0; i < DealAreaDamage(); i++)
            {
                if (_hits[i].transform.TryGetComponent(out IHealth health))
                    health.TakeDamage(enemy.Damage * _damageMultiplier);
            }
        }

        public void TryFinishMeleeState()
        {
            _enemyZoneCheker.TryFinishState();
        }

        private int DealAreaDamage()
        {
            return Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _hits, _explosionMask);
        }
    }
}