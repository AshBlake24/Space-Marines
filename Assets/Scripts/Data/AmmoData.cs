using System;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class AmmoData
    {
        public bool InfinityAmmo;
        public int CurrentAmmo;

        public AmmoData(bool infinityAmmo, int currentAmmo)
        {
            InfinityAmmo = infinityAmmo;
            CurrentAmmo = currentAmmo;
        }
    }
}