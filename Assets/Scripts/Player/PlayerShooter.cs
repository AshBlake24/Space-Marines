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
        private const float DefaultAttackSpeedMultiplier = 1f;
        
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private Transform _weaponSpawnPoint;
        private IInputService _inputService;
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private float _weaponSwitchCooldown;
        private float _lastWeaponSwitchTime;
        private float _lastShotTime;
        private float _attackSpeedMultiplier;

        public event Action<IWeapon> WeaponChanged;
        
        private void OnGUI()
        {
            if (GUI.Button(new Rect(30, 150, 100, 35), "Refill ammo"))
            {
                foreach (IWeapon weapon in _weapons)
                {
                    if (weapon is RangedWeapon rangedWeapon)
                        rangedWeapon.AmmoData.CurrentAmmo = rangedWeapon.AmmoData.MaxAmmo;
                }
            }
        }

        private void Awake() =>
            _inputService = AllServices.Container.Single<IInputService>();

        public void Construct(List<IWeapon> weapons, float weaponSwitchCooldown, Transform weaponSpawnPoint)
        {
            _weapons = weapons;
            _weaponSwitchCooldown = weaponSwitchCooldown;
            _weaponSpawnPoint = weaponSpawnPoint;
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;

            if (_weapons.Count > 0)
            {
                _currentWeaponIndex = 0;
                SetWeapon();
            }
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
            if (_currentWeapon == null)
                return;
            
            if (_inputService.IsAttackButtonUp() == false)
                return;

            float attackRate = _currentWeapon.Stats.AttackRate / _attackSpeedMultiplier;
            
            if (Time.time < (attackRate + _lastShotTime))
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
            if (_weapons.Count <= 0)
                return;

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

        public void SetAttackSpeedMultiplier(float attackSpeedMultiplier) => 
            _attackSpeedMultiplier = attackSpeedMultiplier;

        public void ResetAttackSpeedMultiplier() => 
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;
    }
}