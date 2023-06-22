using Roguelike.UI.Windows.Panels;

namespace Roguelike.UI.Windows
{
    public class StatisticsWindow : BaseWindow
    {
        private StatisticsPanel _statisticsPanel;

        public void Construct(StatisticsPanel statisticsPanel)
        {
            _statisticsPanel = statisticsPanel;
        }
        
        protected override void Initialize()
        {
            TimeService.PauseGame();
            _statisticsPanel.InitStats();
        }
    }
}