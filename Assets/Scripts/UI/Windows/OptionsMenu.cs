using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class OptionsMenu : BaseWindow
    {
        protected override void Initialize() => 
            TimeService.PauseGame();
    }
}