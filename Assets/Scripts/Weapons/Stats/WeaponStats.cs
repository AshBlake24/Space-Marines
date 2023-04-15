using Roguelike.StaticData.Weapons;

namespace Roguelike.Weapons.Stats
{
    public abstract class WeaponStats
    {
        private readonly string _name;
        private readonly float _attackRate;
        private readonly WeaponSize _weaponSize;
        private readonly WeaponType _weaponType;
        private readonly WeaponId _id;

        protected WeaponStats(WeaponStaticData weaponData)
        {
            _name = weaponData.Name;
            _attackRate = weaponData.AttackRate;
            _weaponSize = weaponData.WeaponSize;
            _weaponType = weaponData.WeaponType;
            _id = weaponData.WeaponId;
        }
        
        public string Name => _name;
        public float AttackRate => _attackRate;
        public WeaponSize Size => _weaponSize;
        public WeaponId ID => _id;
        public WeaponType Type => _weaponType;
    }
}