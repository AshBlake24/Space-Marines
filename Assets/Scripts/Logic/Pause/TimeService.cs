using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Logic.Pause
{
    public class TimeService : ITimeService
    {
        private const float DefaultTimeScale = 1.0f;
        private const float PausedTimeScale = 0f;

        public bool IsPaused => Mathf.Approximately(Time.timeScale, PausedTimeScale);
        public bool IsPauseMenuOpen => PauseMenu != null;
        public PauseMenu PauseMenu { get; private set; }

        public void PauseGame(PauseMenu pauseMenu = null)
        {
            Time.timeScale = PausedTimeScale;
            
            if (PauseMenu == null)
                PauseMenu = pauseMenu;
        }

        public void ResumeGame()
        {
            Time.timeScale = DefaultTimeScale;
            PauseMenu = null;
        }
    }
}