using System;
using System.Collections.Generic;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
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

        public PlayerProgress(string startLevel, LevelId startStage, IEnumerable<IWeapon> startWeapons, CharacterId startCharacter)
        {
            State = new State();
            WorldData = new WorldData(startLevel, startStage);
            PlayerWeapons = new PlayerWeapons(startWeapons);
            Character = startCharacter;
        }
    }
}