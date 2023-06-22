using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemies.Traps
{
    [RequireComponent(typeof(Collider))]
    public class Spear : MonoBehaviour
    {
        private const float EffectHeight = 1.3f;
        
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _bloodEffect;
        
        private int _damage;

        public void Init(int damage) => 
            _damage = damage;

        public void SwitchState(bool isActive) => 
            _collider.enabled = isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.TakeDamage(_damage);
                Vector3 playerPosition = playerHealth.transform.position;
                Vector3 effectPosition = new(playerPosition.x, EffectHeight, playerPosition.z);
                Instantiate(_bloodEffect, effectPosition, Quaternion.identity);
            }
        }
    }
}