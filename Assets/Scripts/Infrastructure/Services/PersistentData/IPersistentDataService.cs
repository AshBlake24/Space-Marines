using Roguelike.Data;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public interface IPersistentDataService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
        bool IsResetting { get; }
        void UpdateStatistics();
        void Reset();
        void ResetAllProgress();
        void ResetTutorial();
    }
}