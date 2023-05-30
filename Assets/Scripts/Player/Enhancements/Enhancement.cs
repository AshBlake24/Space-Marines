using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public class Enhancement
    {
        public readonly EnhancementStaticData Data;

        public int CurrentTier { get; private set; }
        public int Value => Data.ValuesOnTiers[CurrentTier];
        public bool CanUpgrade => CurrentTier < Data.MaxTier;

        public Enhancement(EnhancementStaticData enhancementData)
        {
            Data = enhancementData;
            CurrentTier = 0;
        }

        public void Upgrade()
        {
        }
    }
}