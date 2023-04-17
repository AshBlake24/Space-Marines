using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        [Header("Weapon")]
        public WeaponId Id;
        public WeaponType Type;
        public WeaponSize Size;
        public GameObject WeaponPrefab;
        public string Name;
        public float AttackRate;
    }
}