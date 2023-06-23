using Cinemachine;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.UI.Elements;
using System;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class BossDieState : DieState
    {
        private Transform _cameraPoint;
        private ActorUI _healthBar;
        private CinemachineVirtualCamera _bossCamera;
        private BossStateMachine _stateMashine;

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            _stateMashine = GetComponent<BossStateMachine>();

            DestroyHealthBar();

            AvtivateCamera();

            base.Enter(curentEnemy, enemyAnimator);
        }

        public override void Die()
        {
            Destroy(_bossCamera);

            base.Die();
        }

        private void DestroyHealthBar()
        {
            _healthBar = _stateMashine.BossRoot.HealthBar;
            
            if ( _healthBar != null )
                Destroy(_healthBar.gameObject);
        }

        private void AvtivateCamera()
        {
            _bossCamera = _stateMashine.BossRoot.Camera;
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
    }
}