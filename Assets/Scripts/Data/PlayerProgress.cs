using System;
using System.Collections.Generic;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public WeaponsData WeaponsData;

        public PlayerProgress(string initialLevel, IWeapon startWeapon)
        {
            WorldData = new WorldData(initialLevel);
            WeaponsData = new WeaponsData(startWeapon);
        }
    }
}