using Roguelike.Data;
using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Localization;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Windows.Confirmations;
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
        private IWindowService _windowService;

        public void Construct(IStaticDataService staticDataService, ISceneLoadingService sceneLoadingService, 
            IWindowService windowService)
        {
            _staticData = staticDataService;
            _sceneLoadingService = sceneLoadingService;
            _windowService = windowService;
        }
        
        protected override void Initialize()
        {
            InitNewGameButton();
            InitContinueButton();
        }

        protected override void SubscribeUpdates() => 
            Settings.LanguageChanged += OnLanguageChanged;

        protected override void Cleanup()
        {
            base.Cleanup();
            _newGameButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            Settings.LanguageChanged -= OnLanguageChanged;
        }

        private void InitContinueButton()
        {
            TextMeshProUGUI continueButtonText = _continueButton.GetComponentInChildren<TextMeshProUGUI>();
            
            if (ProgressService.PlayerProgress.State.Dead)
                ProgressService.Reset();

            if (ProgressService.PlayerProgress.WorldData.CurrentLevel == LevelId.Dungeon)
            {
                string currentStage = ProgressService.PlayerProgress.WorldData.CurrentStage.ToLabel();
                continueButtonText.text = $"{LocalizedConstants.Continue.Value}\n({currentStage})";
                _continueButton.interactable = true;
                _continueButton.onClick.AddListener(OnContinueGame);
            }
            else
            {
                continueButtonText.text = $"{LocalizedConstants.Continue.Value}";
                _continueButton.interactable = false;
            }
        }

        private void InitNewGameButton() => 
            _newGameButton.onClick.AddListener(OnNewGame);

        private void OnNewGame()
        {
            if (ProgressService.PlayerProgress.WorldData.CurrentLevel == LevelId.Dungeon)
            {
                ConfirmationWindow confirmationWindow = _windowService.Open(WindowId.StartNewGameWindow)
                    .GetComponent<ConfirmationWindow>();

                confirmationWindow.Confirmed += OnConfirmed;
                confirmationWindow.Closed += OnClosed;
            }
            else
            {
                StartNewGame();
            }
        }

        private void StartNewGame()
        {
            ProgressService.UpdateStatistics();
            ProgressService.PlayerProgress.WorldData = new WorldData(
                _staticData.GameConfig.StartLevel);
            
            LoadLevel();
        }

        private void OnLanguageChanged() => 
            InitContinueButton();

        private void OnContinueGame() => 
            LoadLevel();

        private void LoadLevel() => 
            _sceneLoadingService.Load(ProgressService.PlayerProgress.WorldData.CurrentLevel);

        private void OnConfirmed(ConfirmationWindow confirmationWindow)
        {
            OnClosed(confirmationWindow);
            StartNewGame();
        }
        
        private void OnClosed(BaseWindow window)
        {
            window.Closed -= OnClosed;
            
            if (window is ConfirmationWindow confirmationWindow)
                confirmationWindow.Confirmed -= OnConfirmed;
        }
    }
}