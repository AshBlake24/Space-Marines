using Roguelike.Data;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public interface IPersistentDataService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
        void Reset();
    }
}