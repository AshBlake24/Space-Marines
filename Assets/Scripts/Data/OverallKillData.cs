using System;
using Roguelike.StaticData.Levels;

namespace Roguelike.Data
{
    [Serializable]
    public class OverallKillData
    {
        public LevelId Id;
        public int KilledMonsters;
        public int KilledBosses;

        public OverallKillData(LevelId id)
        {
            Id = id;
        }
    }
}