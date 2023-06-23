using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Statistics
    {
        public KillData KillData;
        public Favourites Favourites;
        public CollectablesData CollectablesData;

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

                score += (int)Math.Floor(CollectablesData.CoinsCollected * CollectablesData.CoinsScoreMultiplicator);

                return score;
            }
        }
    }
}