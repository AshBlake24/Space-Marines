
using Roguelike.Roguelike.Enemies.Animators;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class ExplosionState : EnemyState
    {
        [SerializeField] private ParticleSystem _effects;

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            animator.PlayAttack();
        }

        public void Explosion()
        {
            EnemyHealth health = GetComponentInChildren<EnemyHealth>();
            health.TakeDamage(health.MaxHealth);

            enemy.Target.TakeDamage(enemy.Damage);

            if (_effects != null)
                _effects.Play();
        }
    }
}
