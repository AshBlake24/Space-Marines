using Roguelike.StaticData.Weapons;

namespace Roguelike.Weapons.Stats
{
    public abstract class WeaponStats
    {
        private readonly string _name;
        private readonly float _attackRate;
        private readonly WeaponSize _weaponSize;
        
        protected WeaponStats(WeaponStaticData weaponData)
        {
            _name = weaponData.Name;
            _attackRate = weaponData.AttackRate;
            _weaponSize = weaponData.WeaponSize;
        }
        
        public string Name => _name;
        public float AttackRate => _attackRate;
        public WeaponSize Size => _weaponSize;
    }
}