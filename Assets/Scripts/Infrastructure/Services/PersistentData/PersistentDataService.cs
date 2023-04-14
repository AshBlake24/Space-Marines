using Roguelike.Data;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public class PersistentDataService : IPersistentDataService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}