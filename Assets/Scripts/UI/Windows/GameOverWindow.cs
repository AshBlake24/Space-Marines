using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Elements.GameOver;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class GameOverWindow : BaseWindow
    {
        private IStaticDataService _staticData;

        public void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitStageViewer();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            TimeService.ResumeGame();
        }

        private void InitStageViewer()
        {
            LevelId currentLevel = ProgressService.PlayerProgress.WorldData.CurrentLevel;

            if (currentLevel == LevelId.Dungeon)
            {
                Sprite characterIcon = _staticData
                    .GetDataById<CharacterId, CharacterStaticData>(ProgressService.PlayerProgress.Character)
                    .Icon;
                
                RegionStaticData regionStaticData = _staticData.GetDataById<LevelId, RegionStaticData>(currentLevel);
                int stagesCount = regionStaticData.StagesCount;
                
                (int stage, string label) = GetLevelInfo(currentLevel);
                
                GetComponentInChildren<GameOverStageViewer>()
                    .Construct(stage, label, stagesCount, characterIcon);
            }
        }

        private (int, string) GetLevelInfo(LevelId currentLevel)
        {
            (int stage, string label) levelInfo;

            if (currentLevel == LevelId.Hub)
            {
                levelInfo.label = "Hub";
                levelInfo.stage = 0;
            }
            else
            {
                levelInfo.label = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();
                levelInfo.stage = (int) ProgressService.PlayerProgress.WorldData.CurrentStage;
            }

            return levelInfo;
        }
    }
}