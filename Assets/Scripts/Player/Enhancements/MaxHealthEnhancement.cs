using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class MaxHealthEnhancement : Enhancement
    {
        private readonly IEnhanceable<int> _playerHealth;
        
        public MaxHealthEnhancement(EnhancementStaticData enhancementStaticData, PlayerHealth playerHealth) : 
            base(enhancementStaticData)
        {
            if (playerHealth is IEnhanceable<int> health)
                _playerHealth = health;
            else
                throw new ArgumentNullException(nameof(playerHealth), $"Not an interface of {typeof(IEnhanceable<int>)}");
        }

        public override void Apply()
        {
            int incrementValue = Data.ValuesOnTiers[CurrentTier - 1];
            
            if (CurrentTier > 1)
                incrementValue -= Data.ValuesOnTiers[CurrentTier - 2];
            
            _playerHealth.Enhance(incrementValue);
        }
    }
}