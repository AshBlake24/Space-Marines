using System;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class RangedWeaponData
    {
        public WeaponId ID;
        public AmmoData AmmoData;

        public RangedWeaponData(WeaponId id, AmmoData ammoData)
        {
            ID = id;
            AmmoData = ammoData;
        }
    }
}