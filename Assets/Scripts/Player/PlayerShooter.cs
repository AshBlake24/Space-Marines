using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerShooter : MonoBehaviour
    {
        private const float DefaultAttackSpeedMultiplier = 1f;
        
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private WeaponSpawnPoint _weaponSpawnPoint;
        private IInputService _inputService;
        private IWeaponFactory _weaponFactory;
        private List<IWeapon> _weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private float _weaponSwitchCooldown;
        private float _lastWeaponSwitchTime;
        private float _lastShotTime;
        private float _attackSpeedMultiplier;

        public event Action<IWeapon> WeaponChanged;

        public int WeaponsCount => _weapons.Count;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(30, 300, 100, 35), "Refill ammo"))
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

        public void Construct(List<IWeapon> weapons, float weaponSwitchCooldown, 
            WeaponSpawnPoint weaponSpawnPoint, IWeaponFactory weaponFactory)
        {
            _weapons = weapons;
            _weaponFactory = weaponFactory;
            _weaponSwitchCooldown = weaponSwitchCooldown;
            _weaponSpawnPoint = weaponSpawnPoint;
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;

            if (_weapons.Count > 0)
            {
                _currentWeaponIndex = 0;
                SetWeapon();
            }
        }

        private void OnEnable()
        {
            _inputService.WeaponChanged += OnWeaponChanged;
            _playerAnimator.Restarted += OnAnimatorRestarted;
        }

        private void OnDisable()
        {
            _inputService.WeaponChanged -= OnWeaponChanged;
            _playerAnimator.Restarted -= OnAnimatorRestarted;
        }

        private void Start() =>
            WeaponChanged?.Invoke(_currentWeapon);

        private void Update()
        {
            TryAttack();
        }

        public IWeapon TryGetNextWeapon()
        {
            if (WeaponsCount <= 1)
                return null;

            int nextWeaponIndex = _currentWeaponIndex + 1;
            
            if (nextWeaponIndex >= WeaponsCount)
                nextWeaponIndex = 0;

            return _weapons[nextWeaponIndex];
        }

        public IWeapon TryGetPreviousWeapon()
        {
            if (WeaponsCount <= 1)
                return null;

            int nextWeaponIndex = _currentWeaponIndex - 1;
            
            if (nextWeaponIndex < 0)
                nextWeaponIndex = WeaponsCount - 1;

            return _weapons[nextWeaponIndex];
        }

        public bool TryAddWeapon(WeaponId weaponId)
        {
            if (_weapons.SingleOrDefault(x => x.Stats.ID == weaponId) == null)
            {
                IWeapon weapon = _weaponFactory.CreateWeapon(weaponId, _weaponSpawnPoint.transform);
                _weapons.Add(weapon);
                SwitchTo(weapon);

                return true;
            }

            return false;
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

        private void SwitchTo(IWeapon weapon)
        {
            _currentWeaponIndex = _weapons.IndexOf(weapon);
            SetWeapon();
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

        private void OnAnimatorRestarted() => 
            _playerAnimator.SetWeapon(_currentWeapon.Stats.Size);
    }
}