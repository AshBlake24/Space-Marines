using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Weapons.Stats
{
    public abstract class WeaponStats
    {
        private readonly string _name;
        private readonly float _attackRate;
        private readonly WeaponSize _weaponSize;
        private readonly WeaponType _type;
        private readonly WeaponId _id;
        private readonly Sprite _icon;
        private readonly GameObject _prefab;

        protected WeaponStats(WeaponStaticData weaponData)
        {
            _name = weaponData.Name;
            _attackRate = weaponData.AttackRate;
            _weaponSize = weaponData.Size;
            _type = weaponData.Type;
            _id = weaponData.Id;
            _icon = weaponData.Icon;
            _prefab = weaponData.WeaponPrefab;
        }
        
        public string Name => _name;
        public float AttackRate => _attackRate;
        public WeaponSize Size => _weaponSize;
        public WeaponType Type => _type;
        public WeaponId ID => _id;
        public Sprite Icon => _icon;
        public GameObject Prefab => _prefab;

    }
}