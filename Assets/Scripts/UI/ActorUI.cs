using Roguelike.Player;
using Roguelike.UI.Elements;
using UnityEngine;

namespace Roguelike.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private PlayerHealth _playerHealth;

        private void OnDestroy() =>
            _playerHealth.HealthChanged -= OnHealthChanged;

        public void Construct(PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
            _playerHealth.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.SetValue(_playerHealth.CurrentHealth, _playerHealth.MaxHealth);
        }
    }
}