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
        [SerializeField] CinemachineVirtualCamera _bossCamera;
        [SerializeField] ActorUI _bossUI;
        [SerializeField] Transform _cameraPoint;
        [SerializeField] GameObject _portal;
        [SerializeField] Transform _portalSpawnPoint;
        [SerializeField] Transform _appearanceStopPosition;

        private NavMeshAgent _agent;
        private CinemachineVirtualCamera _currentCamera;
        private Transform _previousCameraFollower;

        private void Update()
        {
            if (_agent != null)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    _agent.isStopped = true;
                    animator.Move(0, _agent.isStopped);

                    _portal.SetActive(false);

                    Debug.Log("Выход");
                }
            }
        }

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(curentEnemy, enemyAnimator);

            _currentCamera = Instantiate(_bossCamera);
            _portal = Instantiate(_portal, _portalSpawnPoint.position, _portal.transform.rotation);

            Move();

            if (_currentCamera != null)
            {
                _currentCamera.Follow = _cameraPoint;
                _currentCamera.LookAt = _cameraPoint;
            }
            else
            {
                throw new ArgumentNullException(nameof(CinemachineVirtualCamera));
            }
        }

        public override void Exit(EnemyState nextState)
        {
            ReturnCamera();

            _bossUI.gameObject.SetActive(true);

            base.Exit(nextState);
        }

        public void SetUI(ActorUI bossUI)
        {
            _bossUI= bossUI;
        }

        private void ReturnCamera()
        {
            _currentCamera.gameObject.SetActive(false);
        }

        private void Move()
        {
            _agent = GetComponent<NavMeshAgent>();

            _agent.SetDestination(_appearanceStopPosition.position);
        }
    }
}