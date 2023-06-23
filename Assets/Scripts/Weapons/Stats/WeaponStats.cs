using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Weapons.Stats
{
    public abstract class WeaponStats
    {
        protected WeaponStats(WeaponStaticData weaponData)
        {
            Name = weaponData.Name.Value;
            AttackRate = weaponData.AttackRate;
            Damage = weaponData.Damage;
            Size = weaponData.Size;
            Type = weaponData.Type;
            ID = weaponData.Id;
            IconFullsize = weaponData.FullsizeIcon;
            Prefab = weaponData.WeaponPrefab;
            Rarity = weaponData.Rarity;
        }
        
        public string Name { get; }
        public float AttackRate { get; }
        public int Damage { get; }
        public WeaponSize Size { get; }
        public RarityId Rarity { get; }
        public WeaponType Type { get; }
        public WeaponId ID { get; }
        public Sprite IconFullsize { get; }
        public GameObject Prefab { get; }
    }
}