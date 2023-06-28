using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Windows;

namespace Roguelike.Tutorials
{
    public class TutorialService : ITutorialService
    {
        private readonly IWindowService _windowService;
        private readonly IPersistentDataService _persistentData;

        public TutorialService(IWindowService windowService, IPersistentDataService persistentData)
        {
            _windowService = windowService;
            _persistentData = persistentData;
        }

        public void TryShowTutorial(WindowId windowId)
        {
            if (_persistentData.PlayerProgress.TutorialData.TutorialCompleted)
                return;

            _windowService.Open(windowId);
        }
    }
}