using System;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHealth;
        public int MaxHealth;
        public bool HasResurrected;

        public void ResetState()
        {
            CurrentHealth = MaxHealth;
            HasResurrected = false;
        }
    }
}