using Roguelike.Enemies;
using Roguelike.Enemies.EnemyStates;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Assets.Scripts.Enemies.EnemyStates
{
    public class ChargeState : EnemyState
    {
        private NavMeshAgent _agent;
        private Enemy _enemy;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            if (_enemy == null)
                _enemy = GetComponent<EnemyStateMachine>().Enemy;
        }

        public override void Enter(Enemy curentEnemy)
        {
            base.Enter(curentEnemy);

            _agent.SetDestination(enemy.Target.transform.position);
            _agent.speed = _enemy.Speed * 6;
            _agent.isStopped = false;
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped = true;

            base.Exit(nextState);
        }
    }
}