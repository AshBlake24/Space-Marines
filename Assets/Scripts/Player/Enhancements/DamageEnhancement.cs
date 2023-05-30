using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public class DamageEnhancement : Enhancement
    {
        private readonly PlayerShooter _playerShooter;
        private readonly EnhancementStaticData _data;

        public DamageEnhancement(EnhancementStaticData enhancementStaticData, PlayerShooter playerShooter)
        {
            _data = enhancementStaticData;
            _playerShooter = playerShooter;
            
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        public override bool CanUpgrade => CurrentTier < _data.ValuesOnTiers.Length - 1;

        public override void Upgrade()
        {
            if (CurrentTier >= _data.ValuesOnTiers.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(CurrentTier), "Current tier already reached max level!");

            CurrentTier++;
            RecalculateWeaponDamage();
        }

        public override void Cleanup() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void OnWeaponChanged() => 
            RecalculateWeaponDamage();

        private void RecalculateWeaponDamage() => 
            _playerShooter.CurrentWeapon?.CalculateTotalDamage(_data.ValuesOnTiers[CurrentTier]);
    }
}