using Roguelike.Infrastructure.Services.Input;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class PauseMenu : BaseWindow
    {
        protected override void Initialize() => 
            TimeService.PauseGame();
    }
}