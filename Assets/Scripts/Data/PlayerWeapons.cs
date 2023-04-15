using System;
using System.Collections.Generic;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerWeapons
    {
        public List<RangedWeaponsData> RangedWeapons;

        public PlayerWeapons(IEnumerable<IWeapon> startWeapons)
        {
            RangedWeapons = new List<RangedWeaponsData>();

            InitializeStartWeapons(startWeapons);
        }

        public void SaveRangedWeapon(WeaponId id, int currentAmmo, int currentClipAmmo)
        {
            RangedWeaponsData weapon = RangedWeapons.Find(x => x.ID == id);

            if (weapon != null)
            {
                weapon.CurrentAmmo = currentAmmo;
                weapon.CurrentClipAmmo = currentClipAmmo;
            }
            else
            {
                RangedWeapons.Add(new RangedWeaponsData(id, currentAmmo, currentClipAmmo));
            }
        }

        private void InitializeStartWeapons(IEnumerable<IWeapon> startWeapons)
        {
            foreach (IWeapon weapon in startWeapons)
            {
                if (weapon is RangedWeapon rangedWeapon)
                    SaveRangedWeapon(rangedWeapon.Stats.ID, rangedWeapon.CurrentAmmo, rangedWeapon.CurrentClipAmmo);
            }
        }
    }
}