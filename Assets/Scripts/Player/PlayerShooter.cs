using System;
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
        
        [SerializeField] private PlayerAnimator _playerAnimator;

        private WeaponSpawnPoint _weaponSpawnPoint;
        private IInputService _inputService;
        private IWeaponFactory _weaponFactory;
        private IWeapon[] _weapons;
        private IWeapon _currentWeapon;
        private int _currentWeaponIndex;
        private float _weaponSwitchCooldown;
        private float _lastWeaponSwitchTime;
        private float _lastShotTime;
        private float _attackSpeedMultiplier;

        public event Action<IWeapon> WeaponChanged;

        public int WeaponsCount => _weapons.Length;

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

        public void Construct(IWeapon[] weapons, IWeaponFactory weaponFactory, 
            float weaponSwitchCooldown, WeaponSpawnPoint weaponSpawnPoint)
        {
            _weapons = weapons;
            _weaponFactory = weaponFactory;
            _weaponSwitchCooldown = weaponSwitchCooldown;
            _weaponSpawnPoint = weaponSpawnPoint;
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;
            _currentWeaponIndex = 0;
            SetWeapon();
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

        public void SetAttackSpeedMultiplier(float attackSpeedMultiplier) => 
            _attackSpeedMultiplier = attackSpeedMultiplier;

        public void ResetAttackSpeedMultiplier() => 
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;

        public IWeapon TryGetNextWeapon()
        {
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
            // todo new add logic
            
            if (_weapons.SingleOrDefault(x => x.Stats.ID == weaponId) == null)
            {
                IWeapon weapon = _weaponFactory.CreateWeapon(weaponId, _weaponSpawnPoint.transform);
                //_weapons.Add(weapon);
                SwitchTo(weapon);

                return true;
            }

            return false;
        }

        private void TryAttack()
        {
            if (_inputService.IsAttackButtonUp() == false)
                return;
            
            if (_currentWeapon == null)
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
            _currentWeaponIndex = Array.IndexOf(_weapons, weapon);
            SetWeapon();
        }

        private void SetWeapon()
        {
            _currentWeapon?.Hide();
            _currentWeapon = _weapons[_currentWeaponIndex];
            _currentWeapon?.Show();

            _playerAnimator.SetWeapon(
                _currentWeapon != null 
                    ? _currentWeapon.Stats.Size 
                    : WeaponSize.Unknown);

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

                if (_currentWeaponIndex >= _weapons.Length)
                    _currentWeaponIndex = 0;

                if (_currentWeaponIndex < 0)
                    _currentWeaponIndex = _weapons.Length - 1;

                SetWeapon();
            }
        }

        private void OnAnimatorRestarted() => 
            _playerAnimator.SetWeapon(_currentWeapon.Stats.Size);
    }
}