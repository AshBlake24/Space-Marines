using System;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class FavouriteWeapons
    {
        public WeaponId FavouriteWeapon;
        
        public void SetFavouriteWeapon(WeaponId weaponId)
        {
            FavouriteWeapon = weaponId;
        }
    }
}