using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public LevelId MainMenuScene;
        public LevelId StartLevel;
        public StageId StartStage;
    }
}