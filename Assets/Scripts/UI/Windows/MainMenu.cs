using System;
using System.Linq;
using System.Text;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.States;
using Roguelike.StaticData.Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class MainMenu : BaseWindow
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueButton;
        
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticData;

        public void Construct(GameStateMachine stateMachine, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _staticData = staticData;
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
                string currentStage = ParseCurrentStage();
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

        private string ParseCurrentStage()
        {
            StringBuilder stringBuilder = new();
            
            string stage = ProgressService.PlayerProgress.WorldData.CurrentStage.ToString();
            
            foreach (char symb in stage.Where(char.IsDigit))
                stringBuilder.Append(symb);

            stringBuilder.Insert(1, '-');

            return stringBuilder.ToString();
        }

        private void InitNewGameButton() => 
            _newGameButton.onClick.AddListener(OnNewGame);

        private void OnNewGame()
        {
            ProgressService.PlayerProgress.WorldData = new WorldData(
                _staticData.GameConfig.StartLevel,
                _staticData.GameConfig.StartStage);
            
            _stateMachine.Enter<LoadLevelState, LevelId>(
                ProgressService.PlayerProgress.WorldData.CurrentLevel);
        }

        private void OnContinueGame()
        {
            _stateMachine.Enter<LoadLevelState, LevelId>(
                ProgressService.PlayerProgress.WorldData.CurrentLevel);
        }
    }
}