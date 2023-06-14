using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemies.Traps
{
    [RequireComponent(typeof(Collider))]
    public class Spear : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        private int _damage;

        public void Init(int damage) => 
            _damage = damage;

        public void SwitchState(bool isActive) => 
            _collider.enabled = isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth)) 
                playerHealth.TakeDamage(_damage);
        }
    }
}