using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.UI.Elements;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class PauseMenu : BaseWindow
    {
        [SerializeField] private Transform _content;
        [SerializeField] private EnhancementTooltip _tooltip;
        
        private IUIFactory _uiFactory;

        public void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }
        
        protected override void Initialize()
        {
            TimeService.PauseGame(this);
            InitEnhancementWidgets();
            _tooltip.gameObject.SetActive(false);
        }

        private void InitEnhancementWidgets()
        {
            foreach (EnhancementData enhancementData in ProgressService.PlayerProgress.State.Enhancements)
                _uiFactory.CreateEnhancementWidget(enhancementData, _content, _tooltip);
        }
    }
}