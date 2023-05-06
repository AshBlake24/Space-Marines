using System;
using System.Collections.Generic;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class PlayerWeapons
    {
        public WeaponId[] Weapons;
        public List<RangedWeaponData> RangedWeaponsData;

        public PlayerWeapons(IWeapon startWeapon, int maxWeaponsCount)
        {
            RangedWeaponsData = new List<RangedWeaponData>();
            InitPlayerWeapons(maxWeaponsCount);
            InitStartWeapon(startWeapon);
        }

        public void SaveRangedWeapon(WeaponId id, AmmoData ammoData)
        {
            RangedWeaponData weaponData = RangedWeaponsData.Find(x => x.ID == id);

            if (weaponData != null)
                weaponData.AmmoData = ammoData;
            else
                RangedWeaponsData.Add(new RangedWeaponData(id, ammoData));
        }

        private void InitPlayerWeapons(int maxWeaponsCount)
        {
            Weapons = new WeaponId[maxWeaponsCount];

            for (int i = 0; i < Weapons.Length; i++)
                Weapons[i] = WeaponId.Unknow;
        }

        private void InitStartWeapon(IWeapon startWeapon)
        {
            Weapons[0] = startWeapon.Stats.ID;
            
            if (startWeapon is RangedWeapon rangedWeapon)
                SaveRangedWeapon(
                    rangedWeapon.Stats.ID, 
                    new AmmoData(infinityAmmo: true, currentAmmo: -1, maxAmmo: -1));
        }
    }
}