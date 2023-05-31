using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.Player.Enhancements;
using Roguelike.StaticData.Enhancements;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class EnhancementFactory : IEnhancementFactory
    {
        private readonly IStaticDataService _staticData;

        public EnhancementFactory(IStaticDataService staticData)
        {
            _staticData = staticData;
        }
        
        public IEnhancement CreateEnhancement(EnhancementId id, GameObject player)
        {
            EnhancementStaticData enhancementData = _staticData
                .GetDataById<EnhancementId, EnhancementStaticData>(id);
            
            return ConstructEnhancement(enhancementData, player);
        }

        private IEnhancement ConstructEnhancement(EnhancementStaticData enhancementData, GameObject player)
        {
            return enhancementData.Id switch
            {
                EnhancementId.Damage => CreateDamageEnhancement(enhancementData, player),
                EnhancementId.MovementSpeed => CreateMoveSpeedEnhancement(enhancementData, player),
                EnhancementId.MaxHealth => CreateMaxHealthEnhancement(enhancementData, player),
                _ => throw new ArgumentOutOfRangeException(nameof(enhancementData.Id),
                    "This enhancement does not exist")
            };
        }

        private IEnhancement CreateDamageEnhancement(EnhancementStaticData enhancementData, GameObject player)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            
            return new DamageEnhancement(enhancementData, playerShooter);
        }

        private IEnhancement CreateMoveSpeedEnhancement(EnhancementStaticData enhancementData, GameObject player)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            
            return new MovementSpeedEnhancement(enhancementData, playerMovement);
        }

        private IEnhancement CreateMaxHealthEnhancement(EnhancementStaticData enhancementData, GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            return new MaxHealthEnhancement(enhancementData, playerHealth);
        }
    }
}