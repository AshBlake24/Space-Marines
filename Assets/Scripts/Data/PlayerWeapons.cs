using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerWeapons
    {
        public List<RangedWeaponData> RangedWeapons;

        public PlayerWeapons(IWeapon startWeapon)
        {
            RangedWeapons = new List<RangedWeaponData>();

            InitializeStartWeapon(startWeapon);
        }

        public void InitializeStartWeapon(IWeapon startWeapon)
        {
            RangedWeapons.Clear();
            
            if (startWeapon is RangedWeapon rangedWeapon)
                SaveRangedWeapon(
                    rangedWeapon.Stats.ID, 
                    new AmmoData(infinityAmmo: true, currentAmmo: -1, maxAmmo: -1));
        }

        public void SaveRangedWeapon(WeaponId id, AmmoData ammoData)
        {
            RangedWeaponData weaponData = RangedWeapons.Find(x => x.ID == id);

            if (weaponData != null)
                weaponData.AmmoData = ammoData;
            else
                RangedWeapons.Add(new RangedWeaponData(id, ammoData));
        }

        public IEnumerable<WeaponId> GetWeapons() => 
            RangedWeapons.Select(rangedWeaponData => rangedWeaponData.ID)
                .ToList();
    }
}