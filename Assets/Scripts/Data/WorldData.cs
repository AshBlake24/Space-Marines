using System;

namespace Roguelike.Data
{
    [Serializable]
    public class WorldData
    {
        public string CurrentLevel;

        public WorldData(string initialLevel)
        {
            CurrentLevel = initialLevel;
        }
    }
}