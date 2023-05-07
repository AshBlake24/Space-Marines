using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Loot.Logic
{
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField, Range(1, 5)] private int _healAmount;
        
        private bool _pickedUp;
        
        private void OnTriggerEnter(Collider other)
        {
            if (_pickedUp)
                return;
            
            TryHeal(other);
        }

        private void TryHeal(Component target)
        {
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                _pickedUp = true;
                playerHealth.Heal(_healAmount);
                Destroy(gameObject);
            }
        }
    }
}