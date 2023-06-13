using Roguelike.Player;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Loot.Powerups
{
    [CreateAssetMenu(
        fileName = "Ammo", 
        menuName = "Static Data/Powerups/Effects/AmmoBox", 
        order = 1)]
    public class AmmoBox : PowerupEffect
    {
        public override bool TryApply(GameObject target)
        {
            if (target.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.CurrentWeapon is RangedWeapon rangedWeapon)
                {
                    if (rangedWeapon.TryReload())
                        return true;
                }
            }

            return false;
        }
    }
}