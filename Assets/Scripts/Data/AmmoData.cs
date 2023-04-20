using System;

namespace Roguelike.Data
{
    [Serializable]
    public class AmmoData
    {
        public bool InfinityAmmo;
        public int CurrentAmmo;
        public int MaxAmmo;

        public AmmoData(bool infinityAmmo, int currentAmmo, int maxAmmo)
        {
            InfinityAmmo = infinityAmmo;
            CurrentAmmo = currentAmmo;
            MaxAmmo = maxAmmo;
        }
    }
}