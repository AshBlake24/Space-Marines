using System;

namespace Roguelike.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        int CurrentHealth { get; }
        int MaxHealth { get; }
        void TakeDamage(int damage);
        void Heal(int health);
    }
}