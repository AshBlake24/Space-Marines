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

            if (_persistentData.PlayerProgress.TutorialData.CompletedTutorials.Contains(tutorialId))
                return;
            
            _persistentData.PlayerProgress.TutorialData.CompletedTutorials.Add(tutorialId);
            _windowService.OpenTutorial(tutorialId);
        }
    }
}