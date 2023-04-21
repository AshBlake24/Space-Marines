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
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private Transform _weaponSpawnPoint;

        private IInputService _inputService;
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private float _weaponSwitchCooldown;
        private float _lastWeaponSwitchTime;
        private float _lastShotTime;

        public event Action<IWeapon> WeaponChanged;
        
        public Transform WeaponSpawnPoint => _weaponSpawnPoint;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        public void Construct(List<IWeapon> weapons, float weaponSwitchCooldown)
        {
            _weapons = weapons;
            _weaponSwitchCooldown = weaponSwitchCooldown;
            _currentWeaponIndex = 0;
            SetWeapon();
        }

        private void OnEnable() => 
            _inputService.WeaponChanged += OnWeaponChanged;

        private void OnDisable() => 
            _inputService.WeaponChanged -= OnWeaponChanged;

        private void Start() => 
            WeaponChanged?.Invoke(_currentWeapon);

        private void Update()
        {
            if (_playerHealth.IsAlive == false)
                return;
            
            TryAttack();
        }

        private void TryAttack()
        {
            if (_inputService.IsAttackButtonUp() == false)
                return;

            if (Time.time < (_currentWeapon.Stats.AttackRate + _lastShotTime))
                return;
            
            if (_currentWeapon.TryAttack())
            {
                _lastShotTime = Time.time;
                _playerAnimator.PlayShot();
            }
        }

        private void SetWeapon()
        {
            _currentWeapon?.Hide();
            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon.Show();
            _playerAnimator.SetWeapon(_currentWeapon.Stats.Size);
            
            WeaponChanged?.Invoke(_currentWeapon);
        }

        private void OnWeaponChanged(bool switchToNext)
        {
            if (Time.time > _lastWeaponSwitchTime + _weaponSwitchCooldown)
            {
                _lastWeaponSwitchTime = Time.time;
                
                if (switchToNext)
                    _currentWeaponIndex++;
                else
                    _currentWeaponIndex--;

                if (_currentWeaponIndex >= _weapons.Count)
                    _currentWeaponIndex = 0;

                if (_currentWeaponIndex < 0)
                    _currentWeaponIndex = _weapons.Count - 1;

                SetWeapon();
            }
        }
    }
}