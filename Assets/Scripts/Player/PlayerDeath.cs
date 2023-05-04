using System.Collections;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerDeath : MonoBehaviour, IProgressWriter
    {
        [SerializeField] private float _delayBeforeResurrectionScreen = 1.5f;
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private PlayerShooter _shooter;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerAnimator _animator;

        private IWindowService _windowService;
        private ISaveLoadService _saveLoadService;
        private bool _isDead;

        public void Construct(IWindowService windowService, ISaveLoadService saveLoadService)
        {
            _windowService = windowService;
            _saveLoadService = saveLoadService;
        }

        public void WriteProgress(PlayerProgress progress) =>
            progress.State.Dead = _isDead;

        public void ReadProgress(PlayerProgress progress) =>
            _isDead = progress.State.Dead;

        private void Start() =>
            _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (_isDead == false && _health.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _aim.enabled = false;
            _shooter.enabled = false;
            _movement.enabled = false;
            _interaction.enabled = false;
            _animator.PlayDeath();

            _saveLoadService.SaveProgress();
            StartCoroutine(OpenRessurectionWindowAfterDelay());
        }

        private IEnumerator OpenRessurectionWindowAfterDelay()
        {
            yield return Helpers.GetTime(_delayBeforeResurrectionScreen);

            _windowService.Open(WindowId.Resurrection);
        }
    }
}