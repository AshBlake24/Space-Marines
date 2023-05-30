using Roguelike.Roguelike.Enemies.Animators;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class ChargeState : EnemyState
    {
        private static readonly float ClipDuration = 2.1f;

        [SerializeField, Range(0.5f, 3f)] private float _delayForAnimation;

        private NavMeshAgent _agent;
        private Coroutine _delayCoroutine;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (_delayCoroutine != null)
                StopCoroutine(_delayCoroutine);
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            _agent.SetDestination(enemy.Target.transform.position);
            _agent.speed = enemy.Speed * enemy.ChargeSpeedMultiplication;

            _delayCoroutine = StartCoroutine(Charge());

            animator.SetClipSpeed(ClipDuration / _delayForAnimation);
            animator.Move(_agent.speed, false);
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped = true;

            animator.Move(0, _agent.isStopped);

            base.Exit(nextState);
        }

        private IEnumerator Charge()
        {
            float duration = _delayForAnimation;

            while (duration > 0)
            {
                duration -= Time.deltaTime;

                yield return null;
            }

            _agent.SetDestination(enemy.Target.transform.position);
            _agent.isStopped = false;
        }
    }
}