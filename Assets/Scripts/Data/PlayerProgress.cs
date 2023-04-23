using System;
using System.Collections.Generic;
using Roguelike.StaticData.Characters;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State State;
        public WorldData WorldData;
        public PlayerWeapons PlayerWeapons;
        public CharacterId Character;

        public PlayerProgress(string initialLevel, IEnumerable<IWeapon> startWeapons, CharacterId startCharacter)
        {
            State = new State();
            WorldData = new WorldData(initialLevel);
            PlayerWeapons = new PlayerWeapons(startWeapons);
            Character = startCharacter;
        }
    }
}