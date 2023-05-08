using Roguelike.Data;
using Roguelike.Player;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class AmmoCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentAmmo;

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
            if (_playerShooter.CurrentWeapon is RangedWeapon rangedWeapon)
            {
                UnsubscribeWeapon();
                
                _currentAmmo.enabled = true;
                _ammoData = rangedWeapon.AmmoData;
                _rangedWeapon = rangedWeapon;
                
                SubscribeWeapon();
                ChangeAmmo();
            }
            else
            {
                UnsubscribeWeapon();
                
                _rangedWeapon = null;
                _ammoData = null;
                _currentAmmo.enabled = false;
            }
        }

        private void SubscribeWeapon()
        {
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

        private void ChangeAmmo()
        {
            _currentAmmo.text = _ammoData.InfinityAmmo 
                ? "infinity" 
                : $"{_ammoData.CurrentAmmo}/{_ammoData.MaxAmmo}";
        }
    }
}