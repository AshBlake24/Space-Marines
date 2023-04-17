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
        private bool _isAttacking;

        public Transform WeaponSpawnPoint => _weaponSpawnPoint;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        public void Construct(List<IWeapon> weapons)
        {
            _weapons = weapons;
            _currentWeaponIndex = 0;
            SetWeapon();
        }

        private void OnEnable()
        {
            _inputService.Attacking += OnAttacking;
            _inputService.WeaponChanged += OnWeaponChanged;
        }

        private void OnDisable()
        {
            _inputService.Attacking -= OnAttacking;
            _inputService.WeaponChanged -= OnWeaponChanged;
        }

        private void Update()
        {
            TryAttack();
        }

        private void TryAttack()
        {
            if (_isAttacking == false)
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
        }

        private void OnWeaponChanged(bool switchToNext)
        {
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

        private void OnAttacking(bool isAtacking) => 
            _isAttacking = isAtacking;
    }
}