using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponId, WeaponStaticData> _weapons;

        public void LoadWeapons()
        {
            _weapons = Resources.LoadAll<WeaponStaticData>("Weapons")
                .ToDictionary(x => x.WeaponId);
        }

        public WeaponStaticData GetWeaponData(WeaponId id) => 
            _weapons.TryGetValue(id, out WeaponStaticData staticData) 
                ? staticData 
                : null;
    }
}