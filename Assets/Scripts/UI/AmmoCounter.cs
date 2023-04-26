using System;
using Roguelike.Data;
using Roguelike.Player;
using Roguelike.Weapons;
using TMPro;
using UnityEngine;

namespace Roguelike.UI
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

        private void OnWeaponChaged(IWeapon weapon)
        {
            if (weapon is RangedWeapon rangedWeapon)
            {
                if (_rangedWeapon != null)
                    _rangedWeapon.Fired -= OnFired;

                _currentAmmo.enabled = true;
                _ammoData = rangedWeapon.AmmoData;
                _rangedWeapon = rangedWeapon;
                _rangedWeapon.Fired += OnFired;
                OnFired();
            }
            else
            {
                _rangedWeapon = null;
                _ammoData = null;
                _currentAmmo.enabled = false;
            }
        }

        private void OnFired()
        {
            _currentAmmo.text = _ammoData.InfinityAmmo 
                ? "Ammo: infinity" 
                : $"Ammo: {_ammoData.CurrentAmmo}/{_ammoData.MaxAmmo}";
        }
    }
}