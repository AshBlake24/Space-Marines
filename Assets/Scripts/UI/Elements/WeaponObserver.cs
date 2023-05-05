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
        [SerializeField] private Sprite _emptyWeaponSprite;

        private PlayerShooter _playerShooter;

        public void Construct(PlayerShooter playerShooter)
        {
            _playerShooter = playerShooter;

            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        private void OnDestroy() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void OnWeaponChanged(IWeapon weapon)
        {
            _currentWeapon.sprite = weapon == null
                ? _emptyWeaponSprite
                : weapon.Stats.Icon;

            SetNextWeaponIcon();
            SetPreviousWeaponSprite();
        }

        private void SetPreviousWeaponSprite()
        {
            IWeapon weapon = _playerShooter.TryGetPreviousWeapon();

            _previousWeapon.sprite = weapon != null 
                ? weapon.Stats.Icon 
                : _emptyWeaponSprite;
        }

        private void SetNextWeaponIcon()
        {
            IWeapon weapon = _playerShooter.TryGetNextWeapon();
            
            _nextWeapon.sprite = weapon != null 
                ? weapon.Stats.Icon 
                : _emptyWeaponSprite;
        }
    }
}