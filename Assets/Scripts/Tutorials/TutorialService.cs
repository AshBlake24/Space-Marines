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

        public void TryShowTutorial(TutorialId tutorialId)
        {
            if (_persistentData.PlayerProgress.TutorialData.IsTutorialCompleted)
                return;
            
            _windowService.OpenTutorial(tutorialId);
        }
    }
}