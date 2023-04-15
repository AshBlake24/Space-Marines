using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon, IProgressWriter
    {
        [SerializeField] private Transform _firePoint;

        private RangedWeaponStats _stats;

        public override WeaponStats Stats => _stats;
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

        public void WriteProgress(PlayerProgress progress) => 
            progress.PlayerWeapons.SaveRangedWeapon(Stats.ID, CurrentAmmo, CurrentClipAmmo);

        public void ReadProgress(PlayerProgress progress)
        {
            RangedWeaponsData weaponsData = progress.PlayerWeapons.RangedWeapons.Find(x => x.ID == Stats.ID);
            
            if (weaponsData == null)
                return;

            CurrentAmmo = weaponsData.CurrentAmmo;
            CurrentClipAmmo = weaponsData.CurrentClipAmmo;
        }
    }
}