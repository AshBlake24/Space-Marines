namespace Roguelike.UI.Windows
{
    public class TutorialWindow : BaseWindow
    {
        protected override void Initialize()
        {
            TimeService.PauseGame();
        }
    }
}