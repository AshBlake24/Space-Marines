using Roguelike.Infrastructure.Services;

namespace Roguelike.Tutorials
{
    public interface ITutorialService : IService
    {
        void TryShowTutorial(TutorialId tutorialId);
    }
}