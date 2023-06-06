using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class PauseMenu : BaseWindow
    {
        protected override void Initialize() =>
            TimeService.PauseGame();

        protected override void Cleanup()
        {
            base.Cleanup();
            TimeService.ResumeGame();
        }
    }
}