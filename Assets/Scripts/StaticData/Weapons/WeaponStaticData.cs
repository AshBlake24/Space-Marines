using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        public WeaponId _weaponId;
        public WeaponType WeaponType;
        public WeaponSize WeaponSize;
        public GameObject Prefab;
        public string Name;
        public float AttackRate;
    }
}