using Roguelike.Logic;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private IHealth _health;

        private void OnDestroy() =>
            _health.HealthChanged -= OnHealthChanged;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _healthBar.SetValue(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}