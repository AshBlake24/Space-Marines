using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class DamageEnhancement : Enhancement
    {
        private readonly PlayerShooter _playerShooter;

        public DamageEnhancement(EnhancementStaticData enhancementStaticData, PlayerShooter playerShooter) : 
            base(enhancementStaticData)
        {
            _playerShooter = playerShooter;
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        public override void Upgrade()
        {
            base.Upgrade();
            Apply();
        }

        public override void Apply() => 
            _playerShooter.CurrentWeapon?.CalculateTotalDamage(Data.ValuesOnTiers[CurrentTier - 1]);

        public override void Cleanup() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void OnWeaponChanged() => 
            Apply();
            
    }
}