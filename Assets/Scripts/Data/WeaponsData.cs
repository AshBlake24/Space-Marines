using System;
using System.Collections.Generic;
using Roguelike.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class WeaponsData
    {
        public List<IWeapon> Weapons;

        public WeaponsData(IWeapon startWeapon)
        {
            Weapons = new List<IWeapon>
            {
                startWeapon
            };
        }
    }
}