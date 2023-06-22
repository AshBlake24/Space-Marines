using Roguelike.UI.Windows.Panels;

namespace Roguelike.UI.Windows
{
    public class StatisticsWindow : BaseWindow
    {
        protected override void Initialize()
        {
            TimeService.PauseGame();
            
            GetComponentInChildren<StatisticsPanel>()
                .Construct(ProgressService);
        }
    }
}