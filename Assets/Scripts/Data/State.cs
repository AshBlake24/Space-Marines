using System;
using System.Collections.Generic;
using Roguelike.Player.Enhancements;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public List<EnhancementData> Enhancements;
        public int CurrentHealth;
        public int MaxHealth;
        public bool HasResurrected;
        public bool Dead;

        public void ResetState()
        {
            Enhancements = new List<EnhancementData>();
            CurrentHealth = MaxHealth;
            HasResurrected = false;
            Dead = false;
        }

        public void Resurrect()
        {
            CurrentHealth = MaxHealth;
            HasResurrected = true;
        }

        public void AddEnhancement(Enhancement enhancement)
        {
            if (Enhancements.Exists(item => item.Id == enhancement.Data.Id))
                throw new ArgumentOutOfRangeException(nameof(enhancement), "This enhancement already exists");

            EnhancementData data = new()
            {
                Id = enhancement.Data.Id,
                Tier = enhancement.CurrentTier,
                Value = enhancement.Data.ValuesOnTiers[enhancement.CurrentTier]
            };

            Enhancements.Add(data);
        }
    }
}