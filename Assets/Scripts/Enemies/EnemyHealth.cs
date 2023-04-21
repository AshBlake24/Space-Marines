using System;
using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private int _maxHealth;
        
        private int _currentHealth;

        public event Action HealthChanged;

        public int CurrentHealth
        {
            get => _maxHealth;
            private set
            {
                if (_maxHealth != value)
                {
                    _maxHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public int MaxHealth
        {
            get => _currentHealth;
            private set => _currentHealth = value;
        }

        private void Start() => 
            _currentHealth = _maxHealth;

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);
            
            Debug.Log($"{gameObject.name} take damage. Current health = {_currentHealth}");

            if (_currentHealth <= 0)
                Destroy(gameObject);
        }
    }
}