using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Elements;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class GameOverWindow : BaseWindow
    {
        private IStaticDataService _staticData;

        public void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        protected override void Initialize() => 
            InitStageViewer();

        private void InitStageViewer()
        {
            Sprite characterIcon = _staticData.GetCharacterData(ProgressService.PlayerProgress.Character).Icon;
            string stageLabel;
            int currentStage;

            if (ProgressService.PlayerProgress.WorldData.CurrentLevel == LevelId.Hub)
            {
                stageLabel = "Hub";
                currentStage = 0;
            }
            else
            {
                currentStage = (int) ProgressService.PlayerProgress.WorldData.CurrentStage;
                stageLabel = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();
            }

            GetComponentInChildren<GameOverStageViewer>()
                .Construct(currentStage, characterIcon, stageLabel);
        }
    }
}