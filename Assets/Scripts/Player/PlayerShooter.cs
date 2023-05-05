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

        public bool TryAddWeapon(WeaponId weaponId, Transform weaponPosition)
        {
            if (WeaponExists(weaponId))
                return false;

            if (HasEmptySlots(out int emptySlot) == false)
                emptySlot = DropWeapon(weaponPosition);

            IWeapon weapon = _weaponFactory.CreateWeapon(weaponId, _weaponSpawnPoint.transform);
            _weapons[emptySlot] = weapon;
            SwitchTo(weapon);

            return true;
        }

        private int DropWeapon(Transform weaponPosition)
        {
            if (_currentWeaponIndex == 0)
            {
                _weaponFactory.CreatePickupableWeapon(_weapons[^1].Stats.ID, weaponPosition);
                _weapons[^1] = null;

                return _weapons.Length - 1;
            }
            else
            {
                _weaponFactory.CreatePickupableWeapon(_weapons[_currentWeaponIndex].Stats.ID, weaponPosition);
                _weapons[_currentWeaponIndex] = null;

                return _currentWeaponIndex;
            }
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

        private bool WeaponExists(WeaponId weaponId)
        {
            IWeapon weapon = _weapons.Where(weapon => weapon != null)
                .SingleOrDefault(weapon => weapon.Stats.ID == weaponId);

            return weapon != null;
        }

        private bool HasEmptySlots(out int emptySlot)
        {
            emptySlot = Array.IndexOf(_weapons, null);

            return emptySlot != -1;
        }

        private void OnAnimatorRestarted() =>
            _playerAnimator.SetWeapon(_currentWeapon.Stats.Size);
    }
}