using Roguelike.Roguelike.Enemies.Animators;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class ChargeState : EnemyState
    {
        [SerializeField] private ParticleSystem _effects;
        [SerializeField] private CollisionDamager _collisionDamager;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            _collisionDamager.Init(enemy);

            _agent.SetDestination(enemy.Target.transform.position);
            _agent.speed = enemy.Speed * enemy.ChargeSpeedMultiplication;

            animator.Move(_agent.speed, false);
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped = true;

            animator.Move(0, _agent.isStopped);

            _collisionDamager.Disable();
            _effects.Stop();

            base.Exit(nextState);
        }

        public void Charge()
        {
            _agent.SetDestination(enemy.Target.transform.position);
            _agent.isStopped = false;

            _effects.Play();
        }
    }
}