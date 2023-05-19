﻿using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Enemies;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class DeployState : EnemyState
    {
        private const int DeployRadius = 10;

        [SerializeField] private EnemyId _mine;

        private SpawnPoint _parent;
        private IEnemyFactory _factory;
        private NavMeshAgent _agent;
        private Vector3 _randomPoint;
        private bool _isCorrectPoint;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();

            _factory = AllServices.Container.Single<IEnemyFactory>();

            _parent = GetComponentInParent<SpawnPoint>();
        }

        private void Update()
        {
            if (_isCorrectPoint)
            {
                _agent.SetDestination(_randomPoint);
                _agent.isStopped = false;
            }

            if (transform.position == _randomPoint)
            {
                MineSpawn();
                GetRandomDestination();
            }
        }

        public override void Enter(Enemy curentEnemy)
        {
            base.Enter(curentEnemy);

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

            NavMeshPath currentPath = new NavMeshPath();

            while (!_isCorrectPoint)
            {
                NavMeshHit hit;

                NavMesh.SamplePosition(Random.insideUnitSphere * DeployRadius + transform.position, out hit, DeployRadius, NavMesh.AllAreas);
                _randomPoint = hit.position;

                if (Vector3.Distance(_randomPoint, transform.position) > (DeployRadius-1))
                {
                    _agent.CalculatePath(_randomPoint, currentPath);
                    if (currentPath.status == NavMeshPathStatus.PathComplete)
                        _isCorrectPoint = true;
                }
            }
        }

        private void MineSpawn()
        {
            GameObject mine = _factory.CreateEnemy(transform, _mine, enemy.Target);

            mine.transform.SetParent(_parent.transform);
        }
    }
}