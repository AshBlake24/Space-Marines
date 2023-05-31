using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public interface IEnhancement
    {
        EnhancementId Id { get; }
        int CurrentTier { get; }
        bool CanUpgrade { get; }

        void Apply();
        void Upgrade();
        void Cleanup();
    }
}