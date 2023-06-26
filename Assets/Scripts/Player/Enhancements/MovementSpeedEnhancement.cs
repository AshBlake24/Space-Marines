using System;
using Roguelike.StaticData.Enhancements;
using UnityEngine;
using Object = System.Object;

namespace Roguelike.Player.Enhancements
{
    public sealed class MovementSpeedEnhancement : Enhancement
    {
        private readonly IEnhanceable<int> _playerMovement;
        private ParticleSystem _playerEffect;

        public MovementSpeedEnhancement(EnhancementStaticData enhancementStaticData, int tier,
            PlayerMovement playerMovement) : 
            base(enhancementStaticData, tier)
        {
            if (playerMovement is IEnhanceable<int> movement)
                _playerMovement = movement;
            else
                throw new ArgumentNullException(nameof(playerMovement), 
                    $"Not an interface of {typeof(IEnhanceable<int>)}");
        }

        public override void Apply() => 
            _playerMovement.Enhance(Data.Tiers[CurrentTier - 1].Value);
    }
}