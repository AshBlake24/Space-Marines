using System;

namespace Roguelike.Data
{
    [Serializable]
    public class Statistics
    {
        public KillData KillData;
        public CollectablesData CollectablesData;
        public FavouriteWeapons FavouriteWeapons;
        public FavouriteCharacters FavouriteCharacters;
        public FavouriteEnhancements FavouriteEnhancements;

        public Statistics()
        {
            KillData = new KillData();
            CollectablesData = new CollectablesData();
            FavouriteWeapons = new FavouriteWeapons();
            FavouriteCharacters = new FavouriteCharacters();
            FavouriteEnhancements = new FavouriteEnhancements();
        }
    }
}