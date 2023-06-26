using System;
using Roguelike.Animations.UI;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Elements.GameOver;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class GameCompleteWindow : BaseWindow
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private GameCompleteWindowAnimations _gameCompleteWindowAnimations;

        private IStaticDataService _staticData;
        private ISceneLoadingService _sceneLoadingService;

        public void Construct(IStaticDataService staticData, ISceneLoadingService sceneLoadingService)
        {
            _staticData = staticData;
            _sceneLoadingService = sceneLoadingService;
        }

        protected override void Initialize()
        {
            TimeService.PauseGame();
            InitStageViewer();
            UpdateProgress();
        }

        protected override void SubscribeUpdates()
        {
            _homeButton.onClick.AddListener(OnConfirm);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _homeButton.onClick.RemoveListener(OnConfirm);
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
        }

        private void UpdateProgress()
        {
            ProgressService.PlayerProgress.Statistics.Favourites
                .AddWeapons(ProgressService.PlayerProgress.PlayerWeapons.Weapons);
            
            ProgressService.UpdateStatistics();
            ProgressService.Reset();
        }

        private void OnConfirm() => 
            _sceneLoadingService.Load(_staticData.GameConfig.StartScene);
    }
}