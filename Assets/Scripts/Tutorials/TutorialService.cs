using Roguelike.Data;
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
            Tutorial tutorial = _persistentData.PlayerProgress.TutorialData.GetTutorial(tutorialId);

            if (tutorial == null)
            {
                tutorial = new Tutorial(tutorialId);
                _windowService.OpenTutorial(tutorialId);
                _persistentData.PlayerProgress.TutorialData.SetTutorial(tutorial);
            }
            else if (tutorial.CanShow)
            {
                _windowService.OpenTutorial(tutorialId);
            }
        }
    }
}