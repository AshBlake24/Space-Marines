using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Player.Enhancements
{
    public abstract class Enhancement : IEnhancement
    {
        protected readonly EnhancementStaticData Data;
        
        protected Enhancement(EnhancementStaticData enhancementStaticData)
        {
            Data = enhancementStaticData;
            CurrentTier = 1;
        }

        public EnhancementId Id => Data.Id;
        public int CurrentTier { get; protected set; }
        public bool CanUpgrade => CurrentTier < Data.Tiers.Length;

        public abstract void Apply();

        public virtual void Upgrade()
        {
            if (CurrentTier >= Data.Tiers.Length)
                throw new ArgumentOutOfRangeException(nameof(CurrentTier), "Current tier already reached max level!");

            CurrentTier++;
            Apply();
        }

        public virtual void Cleanup()
        {
        }
    }
}