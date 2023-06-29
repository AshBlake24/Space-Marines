using System;
using System.Collections.Generic;
using Roguelike.Tutorials;

namespace Roguelike.Data
{
    [Serializable]
    public class Statistics
    {
        public KillData KillData;
        public Favourites Favourites;
        public CollectablesData CollectablesData;
        public int CompletedStagesScore;

        public Statistics()
        {
            KillData = new KillData();
            Favourites = new Favourites();
            CollectablesData = new CollectablesData();
        }

        public int PlayerScore
        {
            get
            {
                int score = 0;

                foreach (OverallKillData killData in KillData.OverallKillData)
                {
                    score += killData.KilledMonsters;
                    score += killData.KilledBosses * OverallKillData.BossScoreMultiplicator;
                }
                
                score += CompletedStagesScore;
                score += (int)Math.Floor(CollectablesData.CoinsCollected * CollectablesData.CoinsScoreMultiplicator);

                return score;
            }
        }

        public void OnStageComplete(int score)
        {
            if (score > 0)
                CompletedStagesScore += score;
        }
    }
}