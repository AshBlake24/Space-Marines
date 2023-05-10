using System;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Heal", 
        menuName = "Static Data/Powerups/Effects/First Aid Kit", 
        order = 1)]
    public class FirstAidKit : PowerupEffect
    {
        [SerializeField, Range(1, 5)] private int _healthAmount;

        public override bool TryApply(GameObject target, Action onComplete)
        {
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.Heal(_healthAmount);
                onComplete?.Invoke();
                return true;
            }

            return false;
        }
    }
}