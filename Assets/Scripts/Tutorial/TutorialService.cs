using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Localization;

namespace Roguelike.Tutorial
{
    public class TutorialService : ITutorialService
    {
        private readonly IPersistentDataService _persistentData;
        private IUIFactory _uiFactory;

        public TutorialService(IPersistentDataService persistentData)
        {
            _persistentData = persistentData;
        }

        public void GetUIFactory(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void TryShowTutorialWindow(LocalizedString text)
        {
            if (_persistentData.PlayerProgress.TutorialData.TutorialCompleted == false)
                _uiFactory.CreateTextWindow(text.Value, isTutorial: true);
        }
    }
}