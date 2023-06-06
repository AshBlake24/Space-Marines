using Roguelike.Infrastructure.Services;

namespace Roguelike.Logic.Pause
{
    public interface ITimeService : IService
    {
        bool IsPaused { get; }
        void PauseGame();
        void ResumeGame();
    }
}