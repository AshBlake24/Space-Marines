using System;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Data
{
    [Serializable]
    public class WeaponUsageData
    {
        public WeaponId Id;
        public int UsedTimes;

        public WeaponUsageData(WeaponId weaponId)
        {
            Id = weaponId;
            UsedTimes = 1;
        }
    }
}