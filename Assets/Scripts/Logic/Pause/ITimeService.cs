using Roguelike.Infrastructure.Services;
using Roguelike.UI.Windows;

namespace Roguelike.Logic.Pause
{
    public interface ITimeService : IService
    {
        bool IsPaused { get; }
        bool IsPauseMenuOpen { get; }
        PauseMenu PauseMenu { get; }

        void PauseGame(PauseMenu pauseMenu = null);
        void ResumeGame();
    }
}