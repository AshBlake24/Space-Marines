using Roguelike.StaticData.Enhancements;

namespace Roguelike.Infrastructure.Factory
{
    public interface IEnhancementFactory
    {
        void CreateEnhancement(EnhancementId id);
    }
}