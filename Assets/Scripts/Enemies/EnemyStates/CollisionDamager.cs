using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Enemies.EnemyStates
{
    public class CollisionDamager : MonoBehaviour
    {
        private Enemy _enemy;
        private bool _isActive;

        public void Init(Enemy enemy)
        {
            _enemy= enemy;
            _isActive = true;
        }

        public void Disable()
        {
            _isActive = false;
        }

        public void OnTriggerEnter(Collider other)
        {
            if (_isActive == false)
                return;

            if (other.TryGetComponent<PlayerHealth>(out PlayerHealth player))
            {
                player.TakeDamage(_enemy.Damage);
                enabled = false;
            }
;        }
    }
}