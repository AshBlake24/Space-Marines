using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class PauseMenu : BaseWindow
    {
        private float _defaultTime;

        protected override void OnAwake()
        {
            base.OnAwake();
            _defaultTime = Time.timeScale;
        }

        protected override void Initialize() => 
            PauseGame();

        protected override void Cleanup()
        {
            base.Cleanup();
            ResumeGame();
        }

        private void PauseGame() => 
            Time.timeScale = 0f;

        private void ResumeGame() => 
            Time.timeScale = _defaultTime;
    }
}