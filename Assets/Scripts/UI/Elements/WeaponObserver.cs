using Roguelike.Player;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class WeaponObserver : MonoBehaviour
    {
        [SerializeField] private Image _currentWeapon;
        [SerializeField] private Image _nextWeapon;
        [SerializeField] private Image _previousWeapon;

        private PlayerShooter _playerShooter;

        public void Construct(PlayerShooter playerShooter)
        {
            _playerShooter = playerShooter;
            playerShooter.WeaponChanged += OnWeaponChanged;
        }

        private void OnWeaponChanged(IWeapon weapon)
        {
            _currentWeapon.sprite = weapon.Stats.Icon;

            if (_playerShooter.WeaponsCount > 1)
            {
                SetNextWeaponIcon();
                SetPreviousWeaponSprite();
            }
        }

        private void SetPreviousWeaponSprite() => 
            _previousWeapon.sprite = _playerShooter.TryGetPreviousWeapon().Stats.Icon;

        private void SetNextWeaponIcon() => 
            _nextWeapon.sprite = _playerShooter.TryGetNextWeapon().Stats.Icon;
    }
}