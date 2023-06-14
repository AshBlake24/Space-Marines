using System.Collections.Generic;
using DG.Tweening;
using Roguelike.Player;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Observers
{
    public class PlayerWeaponsViewer : MonoBehaviour
    {
        [Header("Current Weapon")]
        [SerializeField] private RectTransform _currentWeaponRectTransform;
        [SerializeField] private Image _currentWeaponRarityShadow;
        [SerializeField] private Image _currentWeapon;

        [Header("Next Weapon")]
        [SerializeField] private RectTransform _nextWeaponRectTransform;
        [SerializeField] private Image _nextWeaponRarityShadow;
        [SerializeField, Range(0f, 1f)] private float _rarityShadowAlpha;
        
        [Header("Common")]
        [SerializeField] private Sprite _emptyWeaponSprite;
        [SerializeField] private float _swapDuration;

        private PlayerShooter _playerShooter;
        private Dictionary<RarityId, Color> _rarityColors;
        private Vector2 _currentWeaponDefaultPosition;
        private Vector2 _nextWeaponPosition;
        private Sequence _swapAnimationSequence;
        private Color _rarityColor;

        public void Construct(PlayerShooter playerShooter, Dictionary<RarityId, Color> rarityColors)
        {
            _playerShooter = playerShooter;
            _rarityColors = rarityColors;
            _currentWeaponDefaultPosition = _currentWeaponRectTransform.anchoredPosition;
            _nextWeaponPosition = _nextWeaponRectTransform.anchoredPosition - _currentWeaponDefaultPosition;
            
            InitSwapAnimationSequence();
            SetCurrentWeapon();
            
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        private void OnDestroy()
        {
            _playerShooter.WeaponChanged -= OnWeaponChanged;
            _swapAnimationSequence.Kill();
        }

        private void OnWeaponChanged() => SetWeapons();

        private void SetWeapons()
        {
            if (_playerShooter.CurrentWeapon == null)
            {
                _currentWeapon.sprite = _emptyWeaponSprite;
                return;
            }
            
            _swapAnimationSequence.Restart();
            _swapAnimationSequence.Play();
        }

        private void InitSwapAnimationSequence()
        {
            _swapAnimationSequence = DOTween.Sequence();
            
            _swapAnimationSequence.Append(_currentWeaponRectTransform
                .DOAnchorPos(_nextWeaponPosition, _swapDuration)
                .SetEase(Ease.InQuad));
            
            _swapAnimationSequence.AppendCallback(SetCurrentWeapon);
            _swapAnimationSequence.AppendCallback(SetNextWeapon);
            
            _swapAnimationSequence.Append(_currentWeaponRectTransform
                .DOAnchorPos(_currentWeaponDefaultPosition, _swapDuration)
                .SetEase(Ease.OutQuad));
            
            _swapAnimationSequence.SetAutoKill(false);
        }

        private void SetCurrentWeapon()
        {
            _currentWeapon.sprite = _playerShooter.CurrentWeapon.Stats.Icon;
            _currentWeaponRarityShadow.color = _rarityColors[_playerShooter.CurrentWeapon.Stats.Rarity];
        }

        private void SetNextWeapon()
        {
            IWeapon weapon = _playerShooter.TryGetNextWeapon();

            if (weapon != null)
            {
                _nextWeaponRarityShadow.enabled = true;
                _rarityColor = _rarityColors[weapon.Stats.Rarity];
                _rarityColor.a = _rarityShadowAlpha;
                _nextWeaponRarityShadow.color = _rarityColor;
            }
            else
            {
                _nextWeaponRarityShadow.enabled = false;
            }
        }
    }
}