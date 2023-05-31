using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public sealed class MovementSpeedEnhancement : Enhancement
    {
        private readonly IEnhanceable<int> _playerMovement;

        public MovementSpeedEnhancement(EnhancementStaticData enhancementStaticData, PlayerMovement playerMovement) : 
            base(enhancementStaticData)
        {
            if (playerMovement is IEnhanceable<int> movement)
                _playerMovement = movement;
            else
                throw new ArgumentNullException(nameof(playerMovement), 
                    $"Not an interface of {typeof(IEnhanceable<int>)}");
        }

        public override void Apply() => 
            _playerMovement.Enhance(Data.ValuesOnTiers[CurrentTier]);
    }
}