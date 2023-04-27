using System;
using Roguelike.StaticData.Levels;

namespace Roguelike.Data
{
    [Serializable]
    public class WorldData
    {
        public LevelId CurrentLevel;
        public StageId CurrentStage;

        public WorldData(LevelId startLevel, StageId startStage)
        {
            CurrentLevel = startLevel;
            CurrentStage = startStage;
        }
    }
}