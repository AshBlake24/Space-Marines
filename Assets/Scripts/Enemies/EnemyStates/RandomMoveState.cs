using Roguelike.Roguelike.Enemies.Animators;
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

                animator.Move(_agent.speed, _agent.isStopped);
            }

            if (transform.position == _randomPoint)
                GetRandomDestination();
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            GetRandomDestination();
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped=true;

            animator.Move(0, _agent.isStopped);

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
