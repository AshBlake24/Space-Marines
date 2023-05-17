using DG.Tweening.Plugins.Core.PathCore;
using Roguelike.Enemies.EnemyStates;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class ReloadState : EnemyState
    {
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

                NavMesh.SamplePosition(Random.insideUnitSphere*5 + transform.position, out hit, 5, NavMesh.AllAreas);
                _randomPoint = hit.position;

                if (Vector3.Distance(_randomPoint, transform.position) > 3)
                {
                    _agent.CalculatePath(_randomPoint, currentPath);
                    if (currentPath.status == NavMeshPathStatus.PathComplete)
                        _isCorrectPoint = true;
                }
            }
        }
    }
}
