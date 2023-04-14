using Roguelike.Data;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public interface IProgressReader
    {
        void ReadProgress(PlayerProgress progress);
    }
}