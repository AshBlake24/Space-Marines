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

        public int OverallKilledMonsters => OverallKillData.Sum(x => x.KilledMonsters);
        public int OverallKilledBosses => OverallKillData.Sum(x => x.KilledBosses);
        
        public KillData()
        {
            CurrentKillData = new CurrentKillData();
            OverallKillData = new List<OverallKillData>();
        }

        public void TrySaveOverallKills(LevelId levelId)
        {
            if (CurrentKillData.KilledMonsters <= 0 && CurrentKillData.KilledBosses <= 0)
                return;
            
            OverallKillData killData = OverallKillData.SingleOrDefault(data => data.Id == levelId);

            if (killData == null)
            {
                OverallKillData.Add(new OverallKillData(levelId, CurrentKillData.KilledMonsters, CurrentKillData.KilledBosses));
            }
            else
            {
                killData.KilledMonsters += CurrentKillData.KilledMonsters;
                killData.KilledBosses += CurrentKillData.KilledBosses;
            }
            
            CurrentKillData.Reset();
        }
    }
}