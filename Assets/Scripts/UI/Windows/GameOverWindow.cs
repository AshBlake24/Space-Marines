using System;
using Roguelike.Animations.UI;
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
        [SerializeField] private GameOverWindowAnimations _gameOverWindowAnimations;
        
        private IStaticDataService _staticData;

        public void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitStageViewer();
        }

        private void InitStageViewer()
        {
            LevelId currentLevel = ProgressService.PlayerProgress.WorldData.CurrentLevel;

            if (currentLevel != LevelId.Dungeon)
                throw new ArgumentOutOfRangeException(nameof(currentLevel), "Player died out of the dungeon");
            
            Sprite characterIcon = _staticData
                .GetDataById<CharacterId, CharacterStaticData>(ProgressService.PlayerProgress.Character)
                .Icon;
                
            int stagesCount = _staticData.GetDataById<LevelId, RegionStaticData>(currentLevel).StagesCount;
            int stage = (int) ProgressService.PlayerProgress.WorldData.CurrentStage;
            string label = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();

            GetComponentInChildren<GameOverStageViewer>()
                .Construct(label, stagesCount, characterIcon);
            
            _gameOverWindowAnimations.Init(stage);
            _gameOverWindowAnimations.Play();
        }
    }
}