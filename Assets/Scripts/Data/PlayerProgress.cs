using System;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State State;
        public Balance Balance;
        public WorldData WorldData;
        public PlayerWeapons PlayerWeapons;
        public Settings Settings;
        public KillData KillData;
        public CharacterId Character;

        public PlayerProgress(LevelId startLevel, StageId startStage, CharacterId startCharacter, IWeapon startWeapon,
            int maxWeaponsCount)
        {
            State = new State();
            Balance = new Balance();
            WorldData = new WorldData(startLevel, startStage);
            PlayerWeapons = new PlayerWeapons(startWeapon, maxWeaponsCount);
            Settings = new Settings();
            KillData = new KillData();
            Character = startCharacter;
        }
    }
}