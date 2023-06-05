
using Roguelike.Roguelike.Enemies.Animators;
using System.Collections;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class ExplosionState : EnemyState
    {
        private static readonly float ClipDuration = 2.1f;

        [SerializeField, Range(0.5f, 3f)] private float _delayForAnimation;
        [SerializeField] private ParticleSystem _effects;

        private Coroutine _delayCoroutine;

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);
        }

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            enemy.Target.TakeDamage(enemy.Damage);

            animator.SetClipSpeed(ClipDuration / _delayForAnimation);
            animator.PlayAttack();

            _delayCoroutine = StartCoroutine(Explosion());
        }

        private IEnumerator Explosion()
        {
            float duration = _delayForAnimation;

            while (duration > 0)
            {
                duration -= Time.deltaTime;

                yield return null;
            }

            EnemyHealth health = GetComponentInChildren<EnemyHealth>();
            health.TakeDamage(health.MaxHealth);

            _effects.Play();
        }
    }
}
