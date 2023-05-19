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
        public int Damage;
        public float AttackRate;
    }
}