using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Loot.Powerups;
using UnityEngine;

namespace Roguelike.StaticData.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Powerup Static Data", 
        menuName = "Static Data/Powerups/Powerup", 
        order = 1)]
    public class PowerupStaticData : ScriptableObject, IStaticData
    {
        public PowerupId Id;
        public Powerup Prefab;
        public PowerupEffect Effect;
        public ParticleSystem ActiveVFX;
        
        public Enum Key => Id;
    }
}