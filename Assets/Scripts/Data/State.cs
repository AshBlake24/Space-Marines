using System;
using System.Collections.Generic;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public List<EnhancementData> Enhancements;
        public int CurrentHealth;
        public int MaxHealth;
        public bool ResurrectionAdWasShown;
        public bool HasResurrected;
        public bool Dead;

        public State()
        {
            Enhancements = new List<EnhancementData>();
        }

        public void Reset()
        {
            Enhancements = new List<EnhancementData>();
            CurrentHealth = MaxHealth;
            ResurrectionAdWasShown = false;
            HasResurrected = false;
            Dead = false;
        }

        public void Resurrect()
        {
            CurrentHealth = MaxHealth;
            HasResurrected = true;
        }
    }
}