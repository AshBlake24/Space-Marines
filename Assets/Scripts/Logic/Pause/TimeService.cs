using UnityEngine;

namespace Roguelike.Logic.Pause
{
    public class TimeService : ITimeService
    {
        private const float DefaultTimeScale = 1.0f;
        private const float PausedTimeScale = 0f;

        public bool IsPaused => Mathf.Approximately(Time.timeScale, PausedTimeScale);
        
        public void PauseGame() => 
            Time.timeScale = PausedTimeScale;

        public void ResumeGame() => 
            Time.timeScale = DefaultTimeScale;
    }
}