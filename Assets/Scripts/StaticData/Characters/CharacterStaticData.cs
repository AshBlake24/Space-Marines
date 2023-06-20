using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.StaticData.Characters
{
    [CreateAssetMenu(fileName = "New Character", menuName = "Static Data/Player/Character")]
    public class CharacterStaticData : ScriptableObject, IStaticData
    {
        public CharacterId Id;
        public Sprite Icon;
        public GameObject Prefab;
        public WeaponId StartWeapon;
        public SkillId Skill;
        public LocalizedString Description;
        [Range(1, 10)] public int MaxHealth;
        [Range(1, 5)] public int MaxWeaponsCount;

        public Enum Key => Id;
    }
}