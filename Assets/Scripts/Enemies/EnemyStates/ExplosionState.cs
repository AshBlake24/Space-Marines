using System;
using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class ExplosionState : EnemyState
    {
        private const EnemyId Mine = EnemyId.Mine;

        [SerializeField] private ParticleSystem _effects;

        public event Action Exploded;

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            if (enemy.Id == Mine)
                Explosion();
            else
                animator.PlayAttack();
        }

        public void Explosion()
        {
            EnemyHealth health = GetComponentInChildren<EnemyHealth>();
            health.TakeDamage(health.MaxHealth);

            Exploded?.Invoke();
            enemy.Target.TakeDamage(enemy.Damage);

            if (_effects != null)
                _effects.Play();
        }
    }
}
