using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Balance
    {
        public int Coins;

        public Balance()
        {
            Coins = 0;
        }

        public event Action Changed;

        public void AddCoins(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            Coins += coins;
            Changed?.Invoke();
        }

        public void WithdrawCoins(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));
            
            if ((Coins - coins) < 0)
                throw new ArgumentOutOfRangeException(nameof(Coins));

            Coins -= coins;
            Changed?.Invoke();
        }

        public void Reset() => Coins = 0;
    }
}