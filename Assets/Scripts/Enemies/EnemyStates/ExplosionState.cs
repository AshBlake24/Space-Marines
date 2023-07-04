using System;
using Roguelike.Logic;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class ExplosionState : EnemyState
    {
        private readonly Collider[] _hits = new Collider[1];

        [SerializeField] private LayerMask _explosionMask;
        [SerializeField] private ParticleSystem _effects;
        [SerializeField] private float _explosionRadius;

        public event Action Exploded;

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);
            animator.PlayAttack();
        }

        private void Explosion()
        {
            if (_effects != null)
                _effects.Play();

            for (int i = 0; i < Explode(); i++)
            {
                if (_hits[i].transform.TryGetComponent(out IHealth health))
                    health.TakeDamage(enemy.Damage);
            }

            enemy?.Health.TakeDamage(enemy.Health.MaxHealth);
        }

        private int Explode()
        {
            Exploded?.Invoke();
            
            return Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _hits, _explosionMask);
        }
    }
}
