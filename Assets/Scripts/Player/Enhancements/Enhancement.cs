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
            CurrentTier = 0;
        }

        public EnhancementId Id => Data.Id;
        public int CurrentTier { get; protected set; }
        public bool CanUpgrade => CurrentTier < Data.ValuesOnTiers.Length - 1;

        public virtual void Upgrade()
        {
            if (CurrentTier >= Data.ValuesOnTiers.Length - 1)
                throw new ArgumentOutOfRangeException(nameof(CurrentTier), "Current tier already reached max level!");

            CurrentTier++;
        }

        public abstract void Apply();
        public abstract void Cleanup();
    }
}