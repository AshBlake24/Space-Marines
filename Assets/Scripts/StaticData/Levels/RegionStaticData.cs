using UnityEngine;

namespace Roguelike.StaticData.Levels
{
    [CreateAssetMenu(fileName = "New level", menuName = "Static Data/Level/Region")]
    public class RegionStaticData : ScriptableObject
    {
        public LevelId Id;
        public string Name;
        public StageStaticData StartStage;
        
        [Range(1, 5)] 
        public int Difficulty;
    }
}