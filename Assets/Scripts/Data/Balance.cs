using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Balance
    {
        public int Coins;

        public Balance()
        {
            Coins = 1000;
        }

        public event Action Changed;

        public void Add(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));

            Coins += coins;
            Changed?.Invoke();
        }

        public void Withdraw(int coins)
        {
            if (coins < 0)
                throw new ArgumentOutOfRangeException(nameof(coins));
            
            if ((Coins - coins) < 0)
                throw new ArgumentOutOfRangeException(nameof(Coins));

            Coins -= coins;
            Changed?.Invoke();
        }
    }
}