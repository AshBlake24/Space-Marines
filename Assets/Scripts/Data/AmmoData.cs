using System;
using UnityEngine;

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

        public event Action AmmoChanged;
        
        public bool Reload(float ammoAmountMultiplier)
        {
            if (InfinityAmmo || CurrentAmmo == MaxAmmo)
                return false;

            int reloadAmount = (int)(MaxAmmo * ammoAmountMultiplier);
            CurrentAmmo = Mathf.Min(CurrentAmmo + reloadAmount, MaxAmmo);
            AmmoChanged?.Invoke();

            return true;
        }
    }
}