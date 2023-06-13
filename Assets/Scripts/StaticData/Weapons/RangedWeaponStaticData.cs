using Roguelike.StaticData.Projectiles;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    [CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Static Data/Weapons/Ranged Weapon")]
    public class RangedWeaponStaticData : WeaponStaticData
    {
        public int BulletsPerShot;
        public float HorizontalSpread;
        public float VerticalSpread;

        [Header("Ammo")]
        public int MaxAmmo;
        public float ProjectileStartSpeed;
        public ProjectileStaticData Projectile;
        [Range(0.01f, 1f)] public float ReloadingAmmoAmountMultiplier;

        [Header("Audio")] 
        public AudioClip Clip;
    }
}