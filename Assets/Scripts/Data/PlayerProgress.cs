using System;
using System.Collections.Generic;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public PlayerWeapons PlayerWeapons;

        public PlayerProgress(string initialLevel, IEnumerable<IWeapon> startWeapons)
        {
            WorldData = new WorldData(initialLevel);
            PlayerWeapons = new PlayerWeapons(startWeapons);
        }
    }
}