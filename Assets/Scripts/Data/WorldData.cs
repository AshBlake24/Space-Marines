using System;
using Roguelike.StaticData.Levels;

namespace Roguelike.Data
{
    [Serializable]
    public class WorldData
    {
        public LevelId CurrentLevel;
        public RegionId CurrentRegion;
        public StageId CurrentStage;

        public WorldData(LevelId startLevel, RegionId currentRegion = RegionId.Unknown, 
            StageId startStage = StageId.Unknown)
        {
            CurrentLevel = startLevel;
            CurrentRegion = currentRegion;
            CurrentStage = startStage;
        }
    }
}