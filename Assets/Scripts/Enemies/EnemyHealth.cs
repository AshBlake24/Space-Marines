using System;
using Roguelike.Logic;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.Enemies
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _enemyAnimator;

        private int _maxHealth;
        private int _currentHealth;

        public event Action HealthChanged;
        public event Action<EnemyHealth> Died;

        public int CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                if (_currentHealth != value)
                {
                    _currentHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public int MaxHealth
        {
            get => _maxHealth;
            private set => _maxHealth = value;
        }

        private void Start() => 
            _currentHealth = _maxHealth;

        public void Init(EnemyStaticData enemyData)
        {
            _maxHealth = enemyData.Health;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            _currentHealth = Mathf.Max(_currentHealth - damage, 0);

            if (_enemyAnimator != null)
                _enemyAnimator.PlayHit();

            if (_currentHealth <= 0)
            {
                Died?.Invoke(this);
            }
        }

        public void Heal(int health)
        {
            throw new NotImplementedException();
        }
    }
}