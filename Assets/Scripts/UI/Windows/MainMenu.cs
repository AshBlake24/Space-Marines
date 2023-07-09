using System;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.Authorization;
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
        [SerializeField] private Button _authorizeButton;
        
        private IStaticDataService _staticData;
        private ISceneLoadingService _sceneLoadingService;
        private IWindowService _windowService;
        private IAuthorizationService _authorizationService;

        public static event Action Authorized;

        public void Construct(IStaticDataService staticDataService, ISceneLoadingService sceneLoadingService, 
            IAuthorizationService authorizationService, IWindowService windowService)
        {
            _staticData = staticDataService;
            _sceneLoadingService = sceneLoadingService;
            _windowService = windowService;
            _authorizationService = authorizationService;
        }
        
        protected override void Initialize()
        {
            InitNewGameButton();
            InitContinueButton();
            InitAuthorizeButton();
        }

        protected override void SubscribeUpdates() => 
            Settings.LanguageChanged += OnLanguageChanged;

        protected override void Cleanup()
        {
            base.Cleanup();
            _newGameButton.onClick.RemoveAllListeners();
            _continueButton.onClick.RemoveAllListeners();
            _authorizationService.Authorized -= OnAuthorized;
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
        
        private void InitAuthorizeButton()
        {
            if (_authorizationService.IsAuthorized)
               OnAuthorized();
            else
                _authorizationService.Authorized += OnAuthorized;
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
            if (ProgressService.PlayerProgress.TutorialData.IsTutorialCompleted == false)
                ProgressService.ResetTutorial();
            
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
        
        private void OnAuthorized() => Destroy(_authorizeButton.gameObject);
    }
}