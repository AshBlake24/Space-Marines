using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Statistics
    {
        public KillData KillData;
        public int CoinsCollected;
        public int EnhancementsBought;

        public Statistics()
        {
            KillData = new KillData();
        }

        public void AddCoins(int coins)
        {
            if (coins > 0)
                CoinsCollected += coins;
        }
    }
}