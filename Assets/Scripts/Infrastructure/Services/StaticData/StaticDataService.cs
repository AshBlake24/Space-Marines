using System.Collections.Generic;
using System.Linq;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponTypeId, WeaponStaticData> _weapons;

        public void LoadWeapons()
        {
            _weapons = Resources.LoadAll<WeaponStaticData>("Weapons")
                .ToDictionary(x => x.WeaponTypeId);
        }

        public WeaponStaticData GetWeaponData(WeaponTypeId typeId) => 
            _weapons.TryGetValue(typeId, out WeaponStaticData staticData) 
                ? staticData 
                : null;
    }
}