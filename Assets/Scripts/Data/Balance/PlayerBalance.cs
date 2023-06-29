using System;

namespace Roguelike.Data.Balance
{
    [Serializable]
    public class PlayerBalance
    {
        public DungeonBalance DungeonBalance;
        public HubBalance HubBalance;

        public PlayerBalance()
        {
            DungeonBalance = new DungeonBalance();
            HubBalance = new HubBalance();
        }
    }
}