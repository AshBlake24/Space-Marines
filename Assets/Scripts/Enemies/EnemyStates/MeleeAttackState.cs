using Roguelike.Logic;
using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class MeleeAttackState : EnemyState
    {
        private readonly Collider[] _hits = new Collider[1];

        [SerializeField] private LayerMask _explosionMask;
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

            for (int i = 0; i < DealAreaDamage(); i++)
            {
                if (_hits[i].transform.TryGetComponent(out IHealth health))
                    health.TakeDamage(enemy.Damage);
            }
        }

        private int DealAreaDamage()
        {
            return Physics.OverlapSphereNonAlloc(transform.position, _attackRadius, _hits, _explosionMask);
        }
    }
}