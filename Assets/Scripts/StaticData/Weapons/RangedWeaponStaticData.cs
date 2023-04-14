using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "New RangedWeaponData", menuName = "Weapons/Ranged Weapon")]
    public class RangedWeaponStaticData : WeaponStaticData
    {
        public int MaxAmmo;
        public int ClipSize;
        public float ReloadTime;
    }
}