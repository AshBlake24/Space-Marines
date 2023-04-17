using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        public WeaponId Id;
        public WeaponType Type;
        public WeaponSize Size;
        public GameObject Prefab;
        public string Name;
        public float AttackRate;
    }
}