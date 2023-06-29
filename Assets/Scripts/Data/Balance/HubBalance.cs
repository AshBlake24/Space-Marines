using System;

namespace Roguelike.Data.Balance
{
    [Serializable]
    public class HubBalance : Balance
    {
        private const float CoinsConvertMultiplicator = 0.1f;
        
        public void ConvertDungeonToHubCoins(int coins)
        {
            if (coins <= 0)
                return;

            int convertedCoins = (int) Math.Floor(coins * CoinsConvertMultiplicator);
            
            AddCoins(convertedCoins);
        }
    }
}