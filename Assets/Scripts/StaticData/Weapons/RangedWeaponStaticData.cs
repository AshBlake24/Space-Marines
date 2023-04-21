using System;
using Roguelike.Logic;
using Roguelike.StaticData.Projectiles;
using Roguelike.Weapons.Projectiles;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "New RangedWeaponData", menuName = "Static Data/Weapons/Ranged Weapon")]
    public class RangedWeaponStaticData : WeaponStaticData
    {
        [Range(1, 5)] public int BulletsPerShot;
        [Range(0.01f, 0.2f)] public float Spread;

        [Header("Ammo")]
        public bool InfinityAmmo;
        [Range(1, 300)] public int MaxAmmo;
        public ProjectileStaticData Projectile;
    }
}