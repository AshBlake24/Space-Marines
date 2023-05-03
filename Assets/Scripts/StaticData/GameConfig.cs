using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/Game Config")]
    public class GameConfig : ScriptableObject
    {
        public LevelId StartScene;
        public LevelId StartPlayerLevel;
        public StageId StartPlayerStage;
    }
}