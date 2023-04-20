using System;
using System.Collections.Generic;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State State;
        public WorldData WorldData;
        public PlayerWeapons PlayerWeapons;
        

        public PlayerProgress(string initialLevel, IEnumerable<IWeapon> startWeapons)
        {
            State = new State();
            WorldData = new WorldData(initialLevel);
            PlayerWeapons = new PlayerWeapons(startWeapons);
        }
    }

    [Serializable]
    public class State
    {
        public int CurrentHealth;
        public int MaxHealth;

        public void ResetHealth() => CurrentHealth = MaxHealth;
    }
}