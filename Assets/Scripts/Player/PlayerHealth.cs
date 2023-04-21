using System;
using System.Collections;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Logic;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player
{
    [RequireComponent(typeof(PlayerAnimator))]
    public class PlayerHealth : MonoBehaviour, IProgressWriter, IHealth
    {
        [SerializeField] private PlayerAnimator _playerAnimator;
        
        private State _state;
        private float _immuneTimeAfterHit;
        private bool _isImmune;

        public event Action HealthChanged;
        public event Action Died;

        public bool IsAlive => CurrentHealth > 0;

        public int CurrentHealth
        {
            get => _state.CurrentHealth;
            private set
            {
                if (_state.CurrentHealth != value)
                {
                    _state.CurrentHealth = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public int MaxHealth
        {
            get => _state.MaxHealth;
            private set => _state.MaxHealth = value;
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(30, 100, 100, 35), "Take Damage"))
                TakeDamage(1);
        }

        public void Construct(float immuneTimeAfterHit)
        {
            _immuneTimeAfterHit = immuneTimeAfterHit;
            _isImmune = false;
        }

        public void ReadProgress(PlayerProgress progress)
        {
            _state = progress.State;
            HealthChanged?.Invoke();
        }

        public void WriteProgress(PlayerProgress progress)
        {
            progress.State.CurrentHealth = CurrentHealth;
            progress.State.MaxHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (_isImmune || CurrentHealth <= 0)
                return;

            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage must not be less than 0");

            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);

            if (CurrentHealth > 0)
            {
                _playerAnimator.PlayHit();
                StartCoroutine(ImmuneTimer());
            }
            else
            {
                _playerAnimator.PlayDeath();
                Died?.Invoke();
            }
        }

        private IEnumerator ImmuneTimer()
        {
            _isImmune = true;

            yield return Helpers.GetTime(_immuneTimeAfterHit);

            _isImmune = false;
        }
    }
}