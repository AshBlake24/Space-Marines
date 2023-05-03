using Roguelike.StaticData.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Windows
{
    public class MainMenu : BaseWindow
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueButton;

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
            if (ProgressService.PlayerProgress.WorldData.CurrentLevel == LevelId.Dungeon)
            {
                _continueButton.interactable = true;
                _continueButton.onClick.AddListener(OnContinueGame);
            }
            else
            {
                _continueButton.interactable = false;
            }
        }

        private void InitNewGameButton() => 
            _newGameButton.onClick.AddListener(OnNewGame);

        private void OnNewGame()
        {
            throw new System.NotImplementedException();
        }

        private void OnContinueGame()
        {
            throw new System.NotImplementedException();
        }
    }
}