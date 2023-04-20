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

        public PlayerWeapons(IEnumerable<IWeapon> startWeapons)
        {
            RangedWeapons = new List<RangedWeaponData>();

            InitializeStartWeapons(startWeapons);
        }
        
        public IEnumerable<WeaponId> GetWeapons() => 
            RangedWeapons.Select(rangedWeaponData => rangedWeaponData.ID)
                .ToList();

        public void SaveRangedWeapon(WeaponId id, AmmoData ammoData)
        {
            RangedWeaponData weaponData = RangedWeapons.Find(x => x.ID == id);

            if (weaponData != null)
                weaponData.AmmoData = ammoData;
            else
                RangedWeapons.Add(new RangedWeaponData(id, ammoData));
        }

        private void InitializeStartWeapons(IEnumerable<IWeapon> startWeapons)
        {
            foreach (IWeapon weapon in startWeapons)
            {
                if (weapon is RangedWeapon rangedWeapon)
                    SaveRangedWeapon(rangedWeapon.Stats.ID, rangedWeapon.AmmoData);
            }
        }
    }
}