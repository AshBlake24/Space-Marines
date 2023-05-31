using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class MaxHealthEnhancement : Enhancement
    {
        private readonly IEnhanceable<int> _playerHealth;
        
        public MaxHealthEnhancement(EnhancementStaticData enhancementStaticData, int tier, PlayerHealth playerHealth) : 
            base(enhancementStaticData, tier)
        {
            if (playerHealth is IEnhanceable<int> health)
                _playerHealth = health;
            else
                throw new ArgumentNullException(nameof(playerHealth), $"Not an interface of {typeof(IEnhanceable<int>)}");
        }

        public override void Apply()
        {
            int incrementValue = Data.Tiers[CurrentTier - 1].Value;
            
            if (CurrentTier > 1)
                incrementValue -= Data.Tiers[CurrentTier - 2].Value;
            
            _playerHealth.Enhance(incrementValue);
        }
    }
}