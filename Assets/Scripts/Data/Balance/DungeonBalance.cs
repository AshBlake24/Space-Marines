using System;

namespace Roguelike.Data.Balance
{
    [Serializable]
    public class DungeonBalance : Balance
    {
        public void Reset() => Coins = 0;
    }
}