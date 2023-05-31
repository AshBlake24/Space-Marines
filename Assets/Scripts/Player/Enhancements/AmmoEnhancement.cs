using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class AmmoEnhancement : Enhancement
    {
        private readonly PlayerShooter _playerShooter;

        public AmmoEnhancement(EnhancementStaticData enhancementStaticData, int tier, PlayerShooter playerShooter) : 
            base(enhancementStaticData, tier)
        {
            _playerShooter = playerShooter;
            _playerShooter.WeaponChanged += OnWeaponChanged;
        }

        public override void Apply()
        {
            if (_playerShooter.CurrentWeapon is IEnhanceable<float> rangedWeapon)
                rangedWeapon.Enhance(Data.Tiers[CurrentTier - 1].Value);
        }
        
        public override void Cleanup() => 
            _playerShooter.WeaponChanged -= OnWeaponChanged;

        private void OnWeaponChanged() => Apply();
    }
}