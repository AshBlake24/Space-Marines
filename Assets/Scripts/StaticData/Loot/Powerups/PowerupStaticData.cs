using Roguelike.Loot.Powerups;
using UnityEngine;

namespace Roguelike.StaticData.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Powerup Static Data", 
        menuName = "Static Data/Loot/Powerups/Powerup Static Data", 
        order = 1)]
    public class PowerupStaticData : ScriptableObject
    {
        public PowerupId Id;
        public Powerup Prefab;
        public PowerupEffect Effect;
        public ParticleSystem VFX;
    }
}