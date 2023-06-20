using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using Roguelike.StaticData.Loot.Rarity;
using UnityEngine;

namespace Roguelike.StaticData.Weapons
{
    public abstract class WeaponStaticData : ScriptableObject, IStaticData
    {
        [Header("Info")]
        public WeaponId Id;
        public WeaponType Type;
        public WeaponSize Size;
        public RarityId Rarity;
        
        [Header("View")]
        public GameObject WeaponPrefab;
        public GameObject InteractableWeaponPrefab;
        
        [Header("Stats")]
        public Sprite Icon;
        public LocalizedString Name;
        public int Damage;
        public float AttackRate;
        
        public Enum Key => Id;
    }
}