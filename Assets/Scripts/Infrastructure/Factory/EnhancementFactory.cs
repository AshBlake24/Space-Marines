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
        
        public IEnhancement CreateEnhancement(EnhancementId id, int tier, GameObject player)
        {
            EnhancementStaticData enhancementData = _staticData
                .GetDataById<EnhancementId, EnhancementStaticData>(id);
            
            return ConstructEnhancement(enhancementData, tier, player);
        }

        private IEnhancement ConstructEnhancement(EnhancementStaticData enhancementData, int tier, GameObject player)
        {
            return enhancementData.Id switch
            {
                EnhancementId.ViciousProjectiles => CreateViciousProjectiles(enhancementData, tier, player),
                EnhancementId.HermesTread => CreateHermesTread(enhancementData, tier, player),
                EnhancementId.SteelSkin => CreateSteelSkin(enhancementData, tier, player),
                EnhancementId.LuckyAmmo => CreateLuckyAmmo(enhancementData, tier, player),
                _ => throw new ArgumentOutOfRangeException(nameof(enhancementData.Id),
                    "This enhancement does not exist")
            };
        }

        private IEnhancement CreateViciousProjectiles(EnhancementStaticData enhancementData, int tier, GameObject player)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            
            return new DamageEnhancement(enhancementData, tier, playerShooter);
        }

        private IEnhancement CreateHermesTread(EnhancementStaticData enhancementData, int tier,
            GameObject player)
        {
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            
            return new MovementSpeedEnhancement(enhancementData, tier, playerMovement);
        }

        private IEnhancement CreateSteelSkin(EnhancementStaticData enhancementData, int tier,
            GameObject player)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

            return new MaxHealthEnhancement(enhancementData, tier, playerHealth);
        }

        private IEnhancement CreateLuckyAmmo(EnhancementStaticData enhancementData, int tier,
            GameObject player)
        {
            PlayerShooter playerShooter = player.GetComponent<PlayerShooter>();
            
            return new AmmoEnhancement(enhancementData, tier, playerShooter);
        }
    }
}