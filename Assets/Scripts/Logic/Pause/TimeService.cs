using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Logic.Pause
{
    public class TimeService : ITimeService
    {
        private const float DefaultTimeScale = 1.0f;
        private const float PausedTimeScale = 0f;

        public PauseMenu PauseMenu { get; private set; }
        public bool IsPaused => Mathf.Approximately(Time.timeScale, PausedTimeScale);
        
        public void PauseGame(PauseMenu pauseMenu = null)
        {
            Time.timeScale = PausedTimeScale;
            PauseMenu = pauseMenu;
        }

        public void ResumeGame()
        {
            Time.timeScale = DefaultTimeScale;
            PauseMenu = null;
        }
    }
}