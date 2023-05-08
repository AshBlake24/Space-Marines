using Roguelike.StaticData.Powerups;
using UnityEngine;

namespace Roguelike.Powerups
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField] private PowerupEffect _powerupEffect;

        private bool _collected;

        private void OnTriggerEnter(Collider other)
        {
            if (_collected)
                return;
            
            TryApplyPowerup(other.gameObject);
        }

        private void TryApplyPowerup(GameObject target)
        {
            if (_powerupEffect.TryApply(target))
            {
                _collected = true;
                Destroy(gameObject);
            }
            else
            {
                _collected = false;
            }
        }
    }
}