using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class MaxHealthEnhancement : Enhancement
    {
        private readonly PlayerHealth _playerHealth;
        
        public MaxHealthEnhancement(EnhancementStaticData enhancementStaticData, PlayerHealth playerHealth) : 
            base(enhancementStaticData)
        {
            _playerHealth = playerHealth;
        }

        public override void Apply()
        {
            int incrementValue = Data.ValuesOnTiers[CurrentTier - 1];
            
            if (CurrentTier > 1)
                incrementValue -= Data.ValuesOnTiers[CurrentTier - 2];
            
            _playerHealth.IncreaseMaxHealth(incrementValue);
        }
    }
}