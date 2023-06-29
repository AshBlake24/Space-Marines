using System;
using Roguelike.Data.Balance;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State State;
        public PlayerBalance Balance;
        public WorldData WorldData;
        public PlayerWeapons PlayerWeapons;
        public Settings Settings;
        public Statistics Statistics;
        public TutorialData TutorialData;
        public CharacterId Character;

        public PlayerProgress(LevelId startLevel, RegionId startRegion = RegionId.Unknown, StageId startStage = StageId.Unknown)
        {
            State = new State();
            Balance = new PlayerBalance();
            WorldData = new WorldData(startLevel, startRegion, startStage);
            PlayerWeapons = new PlayerWeapons(null, 0);
            Settings = new Settings();
            Statistics = new Statistics();
            TutorialData = new TutorialData();
        }
    }
}