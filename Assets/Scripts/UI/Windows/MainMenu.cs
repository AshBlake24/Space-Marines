using Roguelike.Data;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class MainMenu : BaseWindow
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueButton;
        
        private IStaticDataService _staticData;
        private ISceneLoadingService _sceneLoadingService;

        public void Construct(IStaticDataService staticDataService, ISceneLoadingService sceneLoadingService)
        {
            _staticData = staticDataService;
            _sceneLoadingService = sceneLoadingService;
        }
        
        protected override void Initialize()
        {
            InitNewGameButton();
            InitContinueButton();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _newGameButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
        }

        private void InitContinueButton()
        {
            TextMeshProUGUI continueButtonText = _continueButton.GetComponentInChildren<TextMeshProUGUI>();

            if (ProgressService.PlayerProgress.WorldData.CurrentLevel == LevelId.Dungeon)
            {
                string currentStage = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();
                continueButtonText.text = $"Continue\n({currentStage})";
                _continueButton.interactable = true;
                _continueButton.onClick.AddListener(OnContinueGame);
            }
            else
            {
                continueButtonText.text = "Continue";
                _continueButton.interactable = false;
            }
        }

        

        private void InitNewGameButton() => 
            _newGameButton.onClick.AddListener(OnNewGame);

        private void OnNewGame()
        {
            ProgressService.PlayerProgress.WorldData = new WorldData(
                _staticData.GameConfig.StartPlayerLevel,
                _staticData.GameConfig.StartPlayerStage);
            
            LoadLevel();
        }

        private void OnContinueGame() => 
            LoadLevel();

        private void LoadLevel() => 
            _sceneLoadingService.Load(ProgressService.PlayerProgress.WorldData.CurrentLevel);
    }
}