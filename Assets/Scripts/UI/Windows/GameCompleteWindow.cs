using System;
using Roguelike.Animations.UI;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Elements.GameOver;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class GameCompleteWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private GameCompleteWindowAnimations _gameCompleteWindowAnimations;
        
        private IStaticDataService _staticData;

        public void Construct(IStaticDataService staticData) => 
            _staticData = staticData;

        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitStageViewer();
            ProgressService.UpdateStatistics();
        }

        private void InitStageViewer()
        {
            _title.text = ProgressService.PlayerProgress.State.Dead 
                ? LocalizedConstants.GameOver.Value 
                : LocalizedConstants.RegionCleared.Value;
            
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

            int killedMonsters = ProgressService.PlayerProgress.Statistics.KillData.CurrentKillData.KilledMonsters;
            int coins = ProgressService.PlayerProgress.Balance.Coins;
            
            _gameCompleteWindowAnimations.Init(stage, coins, killedMonsters);
            _gameCompleteWindowAnimations.Play();
            
            ProgressService.PlayerProgress.Statistics.Favourites
                .AddWeapons(ProgressService.PlayerProgress.PlayerWeapons.Weapons);
        }
    }
}