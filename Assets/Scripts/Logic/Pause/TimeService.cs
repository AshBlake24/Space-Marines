using UnityEngine;

namespace Roguelike.Logic.Pause
{
    public class TimeService : ITimeService
    {
        private const float DefaultTimeScale = 1.0f;

        public bool IsPaused => Mathf.Approximately(Time.timeScale, 0f);
        
        public void PauseGame() => 
            Time.timeScale = 0f;

        public void ResumeGame() => 
            Time.timeScale = DefaultTimeScale;
    }
}