namespace Roguelike.UI.Windows
{
    public class StatisticsWindow : BaseWindow
    {
        protected override void Initialize() => 
            TimeService.PauseGame();
    }
}