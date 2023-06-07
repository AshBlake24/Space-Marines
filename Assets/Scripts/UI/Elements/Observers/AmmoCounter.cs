using Roguelike.Data;
using Roguelike.Player;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Observers
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentWeaponAmmo;
        [SerializeField] private Image _infinityAmmoIcon;

        private PlayerShooter _playerShooter;
        private RangedWeapon _rangedWeapon;
        private AmmoData _ammoData;

        public void Construct(PlayerShooter playerShooter)
        {
            _playerShooter = playerShooter;
            _playerShooter.WeaponChanged += OnWeaponChaged;
        }

        private void OnDestroy() =>
            _playerShooter.WeaponChanged -= OnWeaponChaged;

        private void OnWeaponChaged()
        {
            UnsubscribeWeapon();

            if (_playerShooter.CurrentWeapon is RangedWeapon rangedWeapon)
                SetRangedWeapon(rangedWeapon);
            else
                ResetSettings();
        }

        private void SetRangedWeapon(RangedWeapon rangedWeapon)
        {
            _rangedWeapon = rangedWeapon;
            
            if (rangedWeapon.AmmoData.InfinityAmmo == false)
            {
                _infinityAmmoIcon.enabled = false;
                _currentWeaponAmmo.enabled = true;
                _ammoData = rangedWeapon.AmmoData;
                SubscribeRangedWeapon();
                ChangeAmmo();
            }
            else
            {
                _currentWeaponAmmo.enabled = false;
                _infinityAmmoIcon.enabled = true;
            }
        }

        private void ResetSettings()
        {
            _rangedWeapon = null;
            _ammoData = null;
            _currentWeaponAmmo.enabled = false;
            _infinityAmmoIcon.enabled = false;
        }

        private void SubscribeRangedWeapon()
        {
            if (_rangedWeapon.AmmoData.InfinityAmmo)
                return;

            _rangedWeapon.Fired += ChangeAmmo;
            _ammoData.AmmoChanged += ChangeAmmo;
        }

        private void UnsubscribeWeapon()
        {
            if (_rangedWeapon != null)
                _rangedWeapon.Fired -= ChangeAmmo;
            if (_ammoData != null)
                _ammoData.AmmoChanged -= ChangeAmmo;
        }

        private void ChangeAmmo() => 
            _currentWeaponAmmo.text = $"{_ammoData.CurrentAmmo}/{_ammoData.MaxAmmo}";
    }
}