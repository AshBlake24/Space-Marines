using Roguelike.Roguelike.Enemies.Animators;
using System.Collections;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class DieState : EnemyState
    {
        private static readonly float ClipDuration = 2f;

        [SerializeField, Range(0.5f, 3f)] private float _delayForAnimation;

        private Coroutine _delayCoroutine;

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            _delayCoroutine = StartCoroutine(Die());

            animator.SetClipSpeed(ClipDuration / _delayForAnimation);
            animator.PlayDie();
        }

        private IEnumerator Die()
        {
            float duration = _delayForAnimation;

            while (duration > 0)
            {
                duration -= Time.deltaTime;

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}