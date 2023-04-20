using System;

namespace Roguelike.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHealth;
        public int MaxHealth;

        public void ResetHealth() => CurrentHealth = MaxHealth;
    }
}