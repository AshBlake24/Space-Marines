using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private Transform _firePoint;

        private RangedWeaponStats _stats;
        
        public int CurrentAmmo { get; private set; }
        public int CurrentClipAmmo { get; private set; }
        public float LastShotTime { get; private set; }
        
        public void Construct(RangedWeaponStats stats)
        {
            _stats = stats;
            CurrentAmmo = stats.MaxAmmo;
            CurrentClipAmmo = stats.ClipSize;
        }

        public override void Attack()
        {
            throw new System.NotImplementedException();
        }
    }
}