using System;
using System.Collections.Generic;
using System.Linq;
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
            InitTitle();
            
            RegionId currentLevel = ProgressService.PlayerProgress.WorldData.CurrentRegion;

            if (currentLevel == RegionId.Unknown)
                throw new ArgumentOutOfRangeException(nameof(currentLevel), "Player died out of the dungeon");
            
            Sprite characterIcon = _staticData
                .GetDataById<CharacterId, CharacterStaticData>(ProgressService.PlayerProgress.Character)
                .Icon;

            RegionStaticData regionData = _staticData.GetDataById<RegionId, RegionStaticData>(currentLevel);
            List<StageStaticData> stagesData = regionData.Floors
                .SelectMany(floor => floor.Stages)
                .OrderBy(x => x.Id)
                .ToList();

            int stagesCount = regionData.StagesCount;
            int stage = stagesData.FindIndex(stage => stage.Id == ProgressService.PlayerProgress.WorldData.CurrentStage) + 1;
            string label = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();
            
            GetComponentInChildren<GameOverStageViewer>()
                .Construct(label, stagesCount, characterIcon);

            InitViewerAnimations(stage);
        }

        private void InitTitle()
        {
            _title.text = ProgressService.PlayerProgress.State.Dead
                ? LocalizedConstants.GameOver.Value
                : LocalizedConstants.RegionCleared.Value;
        }

        private void InitViewerAnimations(int stage)
        {
            int killedMonsters = ProgressService.PlayerProgress.Statistics.KillData.CurrentKillData.KilledMonsters;
            int coins = ProgressService.PlayerProgress.Balance.Coins;

            _gameCompleteWindowAnimations.Init(stage, coins, killedMonsters);
            _gameCompleteWindowAnimations.Play();
        }

        private void UpdateProgress() => ProgressService.UpdateStatistics();

        private void OnConfirm() => 
            _sceneLoadingService.Load(_staticData.GameConfig.StartLevel);
    }
}