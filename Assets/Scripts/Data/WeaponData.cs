using System;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class WeaponData
    {
        public WeaponId ID;
        public int CurrentAmmo;
        public int CurrentClipAmmo;

        public WeaponData(WeaponId id, int currentAmmo, int currentClipAmmo)
        {
            ID = id;
            CurrentAmmo = currentAmmo;
            CurrentClipAmmo = currentClipAmmo;
        }
    }
}