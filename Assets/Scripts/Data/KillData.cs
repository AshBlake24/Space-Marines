using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Levels;

namespace Roguelike.Data
{
    [Serializable]
    public class KillData
    {
        public List<OverallKillData> OverallKillData;
        public CurrentKillData CurrentKillData;
        
        public KillData()
        {
            CurrentKillData = new CurrentKillData();
            OverallKillData = new List<OverallKillData>();
            InitLevelsKillData();
        }

        public void TrySaveOverallKills(LevelId levelId, int kills)
        {
            if (kills <= 0)
                return;
            
            OverallKillData killData = OverallKillData.SingleOrDefault(data => data.Id == levelId);

            if (killData != null)
                killData.Kills += kills;
        }

        private void InitLevelsKillData()
        {
            OverallKillData.Add(new OverallKillData(LevelId.Dungeon));
        }
    }
}