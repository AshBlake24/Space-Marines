using Roguelike.Data;

namespace Roguelike.Infrastructure.Services.PersistentData
{
    public interface IProgressWriter : IProgressReader
    {
        void WriteProgress(PlayerProgress progress);
    }
}