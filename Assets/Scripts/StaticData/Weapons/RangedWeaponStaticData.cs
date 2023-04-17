using Roguelike.Weapons.Projectiles;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "New RangedWeaponData", menuName = "Static Data/Weapons/Ranged Weapon")]
    public class RangedWeaponStaticData : WeaponStaticData
    {
        [Header("Ranged Weapon Config")]
        public Projectile ProjectilePrefab;
        public int MaxAmmo;
        public int ClipSize;
        public float ReloadTime;
    }
}