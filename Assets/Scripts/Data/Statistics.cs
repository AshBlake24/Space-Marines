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
            CollectablesData = new CollectablesData();
        }
    }
}