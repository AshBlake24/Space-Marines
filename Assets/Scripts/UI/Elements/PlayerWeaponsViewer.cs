using System.Collections.Generic;
using Roguelike.Player;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class PlayerWeaponsViewer : MonoBehaviour
    {
        [SerializeField] private Image _currentWeapon;
        [SerializeField] private Image _nextWeapon;
        [SerializeField] private Image _rarityShadow;
        [SerializeField] private Sprite _emptyWeaponSprite;
        
        private PlayerShooter _playerShooter;
        private Dictionary<RarityId, Color> _rarityColors;

        public void Construct(PlayerShooter playerShooter, Dictionary<RarityId, Color> rarityColors)
        {
            _playerShooter = playerShooter;
            _rarityColors = rarityColors;
            
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        private void OnDestroy() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void OnWeaponChanged()
        {
            

            SetNextWeaponIcon();
        }

        private void SetNextWeaponIcon()
        {
            IWeapon weapon = _playerShooter.TryGetNextWeapon();
            
            _nextWeapon.sprite = weapon != null 
                ? weapon.Stats.Icon 
                : _emptyWeaponSprite;
        }

        private void SetCurrentWeapon()
        {
            if (_playerShooter.CurrentWeapon == null)
            {
                _currentWeapon.sprite = _emptyWeaponSprite;
                return;
            }

            _currentWeapon.sprite = _playerShooter.CurrentWeapon.Stats.Icon;
            _rarityShadow.color = _rarityColors[_playerShooter.CurrentWeapon.Stats.Rarity];
        }
    }
}