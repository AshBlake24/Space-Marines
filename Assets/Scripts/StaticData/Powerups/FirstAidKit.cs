using Roguelike.Player;
using UnityEngine;

namespace Roguelike.StaticData.Powerups
{
    [CreateAssetMenu(fileName = "First Aid Kit", menuName = "Static Data/Powerups/First Aid Kit", order = 5)]
    public class FirstAidKit : PowerupEffect
    {
        [SerializeField, Range(1, 5)] private int _healthAmount;

        public override bool TryApply(GameObject target)
        {
            if (target.TryGetComponent(out PlayerHealth playerHealth))
            {
                playerHealth.Heal(_healthAmount);
                return true;
            }

            return false;
        }
    }
}