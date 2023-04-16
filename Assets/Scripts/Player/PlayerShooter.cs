using System;
using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _weaponSpawnPoint;
        
        private IInputService _inputService;
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private float _lastShotTime;

        public Transform WeaponSpawnPoint => _weaponSpawnPoint;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        public void Construct(List<IWeapon> weapons)
        {
            _weapons = weapons;
            _currentWeaponIndex = 0;
            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.Show();
            _playerAnimator.SetWeapon(_currentWeapon.Stats.Size);
        }

        private void Update()
        {
            if(_inputService.IsAttackButtonUp() == false)
                return;
            
            if (Time.time > (_currentWeapon.Stats.AttackRate + _lastShotTime))
            {
                _lastShotTime = Time.time;
                _playerAnimator.PlayShot();
            }
        }
    }
}