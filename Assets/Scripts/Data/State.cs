using System;
using Roguelike.Data.Enhancements;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public PlayerEnhancements Enhancements;
        public int CurrentHealth;
        public int MaxHealth;
        public bool HasResurrected;
        public bool Dead;

        public void ResetState()
        {
            Enhancements = new PlayerEnhancements();
            CurrentHealth = MaxHealth;
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