using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;

namespace Roguelike.Tutorials
{
    public interface ITutorialService : IService
    {
        void TryShowTutorial(WindowId windowId);
    }
}