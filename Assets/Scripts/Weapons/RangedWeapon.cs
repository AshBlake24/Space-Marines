using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon, IProgressWriter
    {
        [SerializeField] private Transform _firePoint;

        private RangedWeaponStats _stats;
        private float _lastShotTime;

        public override WeaponStats Stats => _stats;
        public int CurrentAmmo { get; private set; }
        public int CurrentClipAmmo { get; private set; }

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

        public void ReadProgress(PlayerProgress progress)
        {
            AmmoData ammoData = progress.PlayerWeapons.Ammo.Find(weapon => weapon.ID == Stats.ID);
            
            if (ammoData != null)
            {
                CurrentAmmo = ammoData.CurrentAmmo;
                CurrentClipAmmo = ammoData.CurrentClipAmmo;
            }
        }

        public void WriteProgress(PlayerProgress progress) => 
            progress.PlayerWeapons.SaveRangedWeapon(Stats.ID, CurrentAmmo, CurrentClipAmmo);
    }
}