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
        public List<WeaponId> Available;
        public List<AmmoData> Ammo;

        public PlayerWeapons(IEnumerable<IWeapon> startWeapons)
        {
            Available = new List<WeaponId>();
            Ammo = new List<AmmoData>();

            InitializeStartWeapons(startWeapons);
        }

        public void SaveRangedWeapon(WeaponId id, bool infinityAmmo, int currentAmmo)
        {
            if (Available.Contains(id) == false)
            {
                Available.Add(id);
                AmmoData ammoData = new()
                {
                    ID = id,
                    InfinityAmmo = infinityAmmo,
                    CurrentAmmo = currentAmmo,
                };
                Ammo.Add(ammoData);
            }
            else
            {
                AmmoData rangedWeapon = Ammo.Single(weapon => weapon.ID == id);
                rangedWeapon.CurrentAmmo = currentAmmo;
            }
        }

        private void InitializeStartWeapons(IEnumerable<IWeapon> startWeapons)
        {
            foreach (IWeapon weapon in startWeapons)
            {
                if (weapon is RangedWeapon rangedWeapon)
                    SaveRangedWeapon(rangedWeapon.Stats.ID, rangedWeapon.InfinityAmmo, rangedWeapon.CurrentAmmo);
                else
                    Available.Add(weapon.Stats.ID);
            }
        }
    }
}