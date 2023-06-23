using Cinemachine;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.UI.Elements;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Roguelike.Enemies.EnemyStates
{
    public class AppearanceState : EnemyState
    {
        [SerializeField] private GameObject _portal;
        [SerializeField] private Transform _portalSpawnPoint;
        [SerializeField] private Transform _appearanceStopPosition;

        private Transform _cameraPoint;
        private ActorUI _healthBar;
        private CinemachineVirtualCamera _bossCamera;
        private BossStateMachine _stateMashine;
        private NavMeshAgent _agent;

        private void Update()
        {
            if (_agent != null)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    _agent.isStopped = true;
                    animator.Move(0, _agent.isStopped);

                    _portal.SetActive(false);
                    _agent = null;
                }
            }
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            _stateMashine = GetComponent<BossStateMachine>();

            if (_portal != null)
            {
                _portal = Instantiate(_portal, _portalSpawnPoint.position, _portal.transform.rotation);
                Move();
            }

            HideHealthBar();
            ActivateCamera();
        }

        private void HideHealthBar()
        {
            _healthBar = _stateMashine.BossRoot.HealthBar;
            _healthBar.gameObject.SetActive(false);
        }

        private void ActivateCamera()
        {
            _bossCamera = _stateMashine.BossRoot.Camera;

            if (_portal != null)
                _cameraPoint = _portalSpawnPoint;
            else
                _cameraPoint = _stateMashine.BossRoot.CameraPoint;

            _bossCamera.gameObject.SetActive(true);

            if (_bossCamera != null)
            {
                _bossCamera.Follow = _cameraPoint;
                _bossCamera.LookAt = _cameraPoint;
            }
            else
            {
                throw new ArgumentNullException(nameof(CinemachineVirtualCamera));
            }
        }

        public override void Exit(EnemyState nextState)
        {
            ReturnCamera();

            if (_healthBar != null)
                _healthBar.gameObject.SetActive(true);

            base.Exit(nextState);
        }

        private void ReturnCamera()
        {
            _bossCamera.gameObject.SetActive(false);
        }

        private void Move()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.SetDestination(_appearanceStopPosition.position);
        }
    }
}