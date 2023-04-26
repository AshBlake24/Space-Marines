using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class MoveState : EnemyState
    {
        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _agent.SetDestination(enemy.Target.transform.position);
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.ResetPath();

            base.Exit(nextState);
        }
    }
}
