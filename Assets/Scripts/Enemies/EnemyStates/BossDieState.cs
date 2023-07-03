using Cinemachine;
using Roguelike.Player;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.UI.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class BossDieState : DieState
    {
        private Transform _cameraPoint;
        private ActorUI _healthBar;
        private CinemachineVirtualCamera _bossCamera;
        private BossStateMachine _stateMashine;
        private List<MonoBehaviour> _playerComponents;

        public override void Enter(Enemy curentEnemy, EnemyAnimator enemyAnimator)
        {
            _stateMashine = GetComponent<BossStateMachine>();
            _playerComponents = new List<MonoBehaviour>();

            DestroyHealthBar();
            AvtivateCamera();

            base.Enter(curentEnemy, enemyAnimator);

            ActivatePlayerInput(false);
        }

        public override void Die()
        {
            Destroy(_bossCamera);
            ActivatePlayerInput(true);

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

        private void ActivatePlayerInput(bool enabled)
        {
            PlayerHealth player = enemy.Target;
            player.SetImmune(!enabled);

            if (_playerComponents.Count == 0)
            {
                _playerComponents.Add(player.GetComponent<PlayerInteraction>());
                _playerComponents.Add(player.GetComponent<PlayerSkill>());
                _playerComponents.Add(player.GetComponent<PlayerMovement>());
                _playerComponents.Add(player.GetComponent<PlayerShooter>());
            }

            foreach (var component in _playerComponents)
            {
                component.enabled = enabled;
            }
        }
    }
}