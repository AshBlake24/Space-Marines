using System;
using Roguelike.StaticData.Loot.Powerups;

namespace Roguelike.Data
{
    [Serializable]
    public class CollectablesData
    {
        public int CoinsCollected;
        public int CoinsSpentOnEnhancements;
        public int PowerupsCollected;
        public int FirstAidKitsCollected;
        public int EnhancementsBought;
        public int ChestsOpened;

        public void AddCoins(int coins)
        {
            if (coins > 0)
                CoinsCollected += coins;
        }

        public void AddCoinsForEnhancements(int coins)
        {
            if (coins > 0)
                CoinsSpentOnEnhancements += coins;
        }

        public void CollectPowerup(PowerupId powerupId)
        {
            PowerupsCollected++;

            if (powerupId == PowerupId.FirstAidKit)
                FirstAidKitsCollected++;
        }
    }
}