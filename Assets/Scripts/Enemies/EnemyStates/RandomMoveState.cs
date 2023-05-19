using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class RandomMoveState : EnemyState
    {
        private const int MoveRadius = 8;

        private NavMeshAgent _agent;
        private Vector3 _randomPoint;
        private bool _isCorrectPoint;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (_isCorrectPoint)
            {
                _agent.SetDestination(_randomPoint);
                _agent.isStopped = false;
            }

            if (transform.position == _randomPoint)
                GetRandomDestination();
        }

        public override void Enter(Enemy curentEnemy)
        {
            base.Enter(curentEnemy);

            GetRandomDestination();
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped=true;

            base.Exit(nextState);
        }

        private void GetRandomDestination()
        {
            _isCorrectPoint = false;

            NavMeshPath currentPath = new NavMeshPath();

            while (!_isCorrectPoint)
            {
                NavMeshHit hit;

                NavMesh.SamplePosition(Random.insideUnitSphere* MoveRadius + transform.position, out hit, MoveRadius, NavMesh.AllAreas);
                _randomPoint = hit.position;

                if (Vector3.Distance(_randomPoint, transform.position) > (MoveRadius-1))
                {
                    _agent.CalculatePath(_randomPoint, currentPath);
                    if (currentPath.status == NavMeshPathStatus.PathComplete)
                        _isCorrectPoint = true;
                }
            }
        }
    }
}
