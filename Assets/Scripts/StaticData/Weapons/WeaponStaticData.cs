using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        public WeaponTypeId WeaponTypeId;
        public GameObject Prefab;
        public string Name;
        public float AttackRate;
    }
}