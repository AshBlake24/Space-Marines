using System;
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
        [SerializeField, Range(0.01f, 1f)] private float _ammoAmountMultiplier;

        public override bool TryApply(GameObject target, Action onComplete)
        {
            if (target.TryGetComponent(out PlayerShooter playerShooter))
            {
                if (playerShooter.CurrentWeapon is RangedWeapon rangedWeapon)
                {
                    if (rangedWeapon.TryReload(_ammoAmountMultiplier))
                    {
                        onComplete?.Invoke();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}