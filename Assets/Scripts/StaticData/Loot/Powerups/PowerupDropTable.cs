using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Powerup Drop Table", 
        menuName = "Static Data/Loot/Drop Tables/Powerup Drop Table", 
        order = 1)]
    public class PowerupDropTable : ScriptableObject
    {
        public List<PowerupConfig> PowerupConfigs;
    }
}