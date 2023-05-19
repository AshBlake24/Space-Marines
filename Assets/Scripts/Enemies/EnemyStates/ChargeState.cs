using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class ChargeState : EnemyState
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override void Enter(Enemy curentEnemy)
        {
            base.Enter(curentEnemy);

            _agent.SetDestination(enemy.Target.transform.position);
            _agent.speed = enemy.Speed * enemy.ChargeSpeedMultiplication;
            _agent.isStopped = false;
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped = true;

            base.Exit(nextState);
        }
    }
}