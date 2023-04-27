using System;
using Roguelike.StaticData.Levels;

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
        public LevelId CurrentStage;

        public WorldData(string startLevel, LevelId startStage)
        {
            CurrentLevel = startLevel;
            CurrentStage = startStage;
        }
    }
}