using System;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerShooter : MonoBehaviour, IProgressWriter
    {
        private const float DefaultAttackSpeedMultiplier = 1f;

        [SerializeField] private PlayerAnimator _playerAnimator;

        private WeaponSpawnPoint _weaponSpawnPoint;
        private IInputService _inputService;
        private IWeaponFactory _weaponFactory;
        private ILootFactory _lootFactory;
        private IWeapon[] _weapons;
        private int _currentWeaponIndex;
        private float _weaponSwitchCooldown;
        private float _lastWeaponSwitchTime;
        private float _lastShotTime;
        private float _attackSpeedMultiplier;

        public event Action WeaponChanged;
        public event Action<IWeapon> DroppedWeapon;

        public IWeapon CurrentWeapon { get; private set; }
        public bool Boosted { get; private set; }

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

        public void Construct(IWeapon[] weapons, IWeaponFactory weaponFactory, ILootFactory lootFactory,
            float weaponSwitchCooldown, WeaponSpawnPoint weaponSpawnPoint)
        {
            _weapons = weapons;
            _weaponFactory = weaponFactory;
            _lootFactory = lootFactory;
            _weaponSwitchCooldown = weaponSwitchCooldown;
            _weaponSpawnPoint = weaponSpawnPoint;
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;
            _currentWeaponIndex = 0;
            Boosted = false;
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
            WeaponChanged?.Invoke();

        private void Update()
        {
            TryAttack();
        }

        public void WriteProgress(PlayerProgress progress)
        {
            WeaponId[] weaponsId = _weapons
                .Select(weapon => weapon == null
                    ? WeaponId.Unknow
                    : weapon.Stats.ID)
                .ToArray();

            progress.PlayerWeapons.Weapons = weaponsId;
            progress.PlayerWeapons.CurrentWeapon = _weapons[_currentWeaponIndex].Stats.ID;
        }

        public void ReadProgress(PlayerProgress progress) => 
            TrySwitchTo(progress.PlayerWeapons.CurrentWeapon);

        public void SetAttackSpeedMultiplier(float attackSpeedMultiplier)
        {
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier + attackSpeedMultiplier;
            Boosted = true;
        }

        public void ResetAttackSpeedMultiplier()
        {
            _attackSpeedMultiplier = DefaultAttackSpeedMultiplier;
            Boosted = false;
        }

        public IWeapon TryGetNextWeapon()
        {
            int nextWeaponIndex = _currentWeaponIndex + 1;

            if (nextWeaponIndex >= _weapons.Length)
                nextWeaponIndex = 0;

            return _weapons[nextWeaponIndex];
        }

        public bool TryAddWeapon(WeaponId weaponId)
        {
            if (WeaponExists(weaponId, out IWeapon availableWeapon))
            {
                if (availableWeapon is RangedWeapon rangedWeapon)
                    return rangedWeapon.TryReload();

                return false;
            }

            if (HasEmptySlots(out int emptySlot) == false)
                emptySlot = DropWeapon();

            IWeapon weapon = _weaponFactory.CreateWeapon(weaponId, _weaponSpawnPoint.transform);
            _weapons[emptySlot] = weapon;
            
            SwitchTo(weapon);

            return true;
        }

        private int DropWeapon()
        {
            int weaponIndexToDrop = _currentWeaponIndex;

            DroppedWeapon?.Invoke(_weapons[weaponIndexToDrop]);

            GameObject droppedWeapon = _lootFactory.CreateConcreteWeapon(
                _weapons[weaponIndexToDrop].Stats.ID,
                transform.position + transform.forward);
            
            droppedWeapon.transform.rotation = transform.rotation;
            _weapons[weaponIndexToDrop] = null;

            return weaponIndexToDrop;
        }

        private void TryAttack()
        {
            if (_inputService.IsAttackButtonUp() == false)
                return;

            if (CurrentWeapon == null)
                return;

            float attackRate = CurrentWeapon.Stats.AttackRate * _attackSpeedMultiplier;

            if (Time.time < (attackRate + _lastShotTime))
                return;

            if (CurrentWeapon.TryAttack())
            {
                _lastShotTime = Time.time;
                _playerAnimator.PlayShot();
            }
        }

        private void TrySwitchTo(WeaponId weaponId)
        {
            IWeapon weapon = _weapons.SingleOrDefault(weapon => weapon != null && weapon.Stats.ID == weaponId);
            
            if (weapon != null)
                SwitchTo(weapon);
        }

        private void SwitchTo(IWeapon weapon)
        {
            _currentWeaponIndex = Array.IndexOf(_weapons, weapon);
            SetWeapon();
        }

        private void SetWeapon()
        {
            CurrentWeapon?.Hide();
            CurrentWeapon = _weapons[_currentWeaponIndex];
            CurrentWeapon?.Show();

            _playerAnimator.SetWeapon(
                CurrentWeapon != null
                    ? CurrentWeapon.Stats.Size
                    : WeaponSize.Unknown);

            WeaponChanged?.Invoke();
        }

        private void OnWeaponChanged(bool switchToNext)
        {
            if ((Time.time > (_lastWeaponSwitchTime + _weaponSwitchCooldown)) == false) 
                return;
            
            int weaponsCount = _weapons.Count(weapon => weapon != null);

            if (weaponsCount <= 1)
                return;

            do
                SwitchWeaponIndex(switchToNext);
            while (_weapons[_currentWeaponIndex] == null);

            SetWeapon();

            _lastWeaponSwitchTime = Time.time;
        }

        private void SwitchWeaponIndex(bool switchToNext)
        {
            if (switchToNext)
                _currentWeaponIndex--;
            else
                _currentWeaponIndex++;

            if (_currentWeaponIndex >= _weapons.Length)
                _currentWeaponIndex = 0;

            if (_currentWeaponIndex < 0)
                _currentWeaponIndex = _weapons.Length - 1;
        }

        private bool WeaponExists(WeaponId weaponId, out IWeapon availableWeapon)
        {
            availableWeapon = _weapons.Where(weapon => weapon != null)
                .SingleOrDefault(weapon => weapon.Stats.ID == weaponId);

            return availableWeapon != null;
        }

        private bool HasEmptySlots(out int emptySlot)
        {
            emptySlot = Array.IndexOf(_weapons, null);

            return emptySlot != -1;
        }

        private void OnAnimatorRestarted() =>
            _playerAnimator.SetWeapon(CurrentWeapon.Stats.Size);
    }
}