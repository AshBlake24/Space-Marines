using Roguelike.Infrastructure.Services;
using Roguelike.UI.Windows;

namespace Roguelike.Logic.Pause
{
    public interface ITimeService : IService
    {
        PauseMenu PauseMenu { get; }
        bool IsPaused { get; }
        void PauseGame(PauseMenu pauseMenu = null);
        void ResumeGame();
    }
}