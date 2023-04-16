using System;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class AmmoData
    {
        public WeaponId ID;
        public int CurrentAmmo;
        public int CurrentClipAmmo;
    }
}