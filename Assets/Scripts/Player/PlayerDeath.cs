using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;

        [SerializeField] private PlayerAim _aim;
        [SerializeField] private PlayerShooter _shooter;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInteraction _interaction;
        [SerializeField] private PlayerAnimator _animator;

        private bool _isDead;

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
        }
    }
}