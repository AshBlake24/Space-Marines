using Roguelike.StaticData.Loot;
using Roguelike.StaticData.Loot.Rarity;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject
    {
        [Header("Stats")]
        public WeaponId Id;
        public WeaponType Type;
        public WeaponSize Size;
        public RarityId Rarity;
        public GameObject WeaponPrefab;
        public GameObject InteractableWeaponPrefab;
        public Sprite Icon;
        public string Name;
        [Range(0, 1000)] public int Damage;
        [Range(0.05f, 2f)] public float AttackRate;
    }
}