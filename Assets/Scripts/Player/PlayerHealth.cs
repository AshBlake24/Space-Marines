using System;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, IProgressWriter
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        
        private State _state;

        public int CurrentHealth
        {
            get => _state.CurrentHealth;
            private set => _state.CurrentHealth = value;
        }
        
        public int MaxHealth
        {
            get => _state.MaxHealth;
            private set => _state.MaxHealth = value;
        }
        
        public void ReadProgress(PlayerProgress progress)
        {
            _state = progress.State;
        }

        public void WriteProgress(PlayerProgress progress)
        {
            progress.State.CurrentHealth = CurrentHealth;
            progress.State.MaxHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            CurrentHealth -= damage;
            _playerAnimator.PlayHit();
        }
    }
}