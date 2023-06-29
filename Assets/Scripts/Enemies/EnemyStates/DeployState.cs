using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class DeployState : EnemyState
    {
        private const int DeployRadiusMax = 10;
        private const int DeployRadiusMin = 8;

        [SerializeField] private EnemyId _mine;

        private SpawnPoint _parent;
        private IEnemyFactory _factory;
        private NavMeshAgent _agent;
        private Vector3 _randomPoint;
        private bool _isCorrectPoint;
        private bool _isReadyToDeploy;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _factory = AllServices.Container.Single<IEnemyFactory>();

            _parent = GetComponentInParent<SpawnPoint>();
        }

        private void Update()
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance && _isReadyToDeploy)
            {
                MineSpawn();
            }
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            GetRandomDestination();
        }

        public override void Exit(EnemyState nextState)
        {
            _agent.isStopped = true;

            base.Exit(nextState);
        }

        private void GetRandomDestination()
        {
            _isCorrectPoint = false;

            NavMeshPath currentPath = new();

            while (!_isCorrectPoint)
            {
                NavMeshHit hit;

                NavMesh.SamplePosition(Random.insideUnitSphere * DeployRadiusMax + transform.position, out hit, DeployRadiusMax, NavMesh.AllAreas);
                _randomPoint = hit.position;

                if (Vector3.Distance(_randomPoint, transform.position) > DeployRadiusMin)
                {
                    if (_agent.CalculatePath(_randomPoint, currentPath))
                        if (currentPath.status == NavMeshPathStatus.PathComplete)
                            _isCorrectPoint = true;
                }
            }

            _agent.SetDestination(_randomPoint);
            _agent.isStopped = false;
            _isReadyToDeploy= true;

            animator.Move(_agent.speed, _agent.isStopped);
        }

        private void MineSpawn()
        {
            animator.PlayAttack();

            _isReadyToDeploy = false;
        }

        public void Spawn()
        {
            GameObject mine = _factory.CreateMinion(transform, _mine, enemy.Target);

            mine.transform.SetParent(_parent.transform);

            mine.GetComponent<EnemyStateMachine>().enabled = true;

            GetRandomDestination();
        }
    }
}