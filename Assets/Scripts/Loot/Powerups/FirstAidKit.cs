using System;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(fileName = "First Aid Kit", menuName = "Static Data/Powerups/First Aid Kit", order = 2)]
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