using System;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHealth;
        public int MaxHealth;
        public bool HasResurrected;
        public bool Dead;

        public void ResetState()
        {
            CurrentHealth = MaxHealth;
            HasResurrected = false;
            Dead = false;
        }
    }
}