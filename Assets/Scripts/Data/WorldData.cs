using System;

namespace Roguelike.Data
{
    [Serializable]
    public class WorldData
    {
        /// <summary>
        /// Displays the current scene in the game. E.g. Hub or Dungeon
        /// </summary>
        public string CurrentLevel;
        
        /// <summary>
        /// Displays the current stage in the dungeon. E.g. 1-1, 1-2, etc.
        /// </summary>
        public string CurrentStage;

        public WorldData(string initialLevel)
        {
            CurrentLevel = initialLevel;
        }
    }
}