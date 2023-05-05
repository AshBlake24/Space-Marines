using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.StaticData.Characters
{
    [CreateAssetMenu(fileName = "New CharacterStaticData", menuName = "Static Data/Character")]
    public class CharacterStaticData : ScriptableObject
    {
        public CharacterId Id;
        public Sprite Icon;
        public GameObject Prefab;
        public WeaponId StartWeapon;
        public SkillId Skill;
        [Range(1, 10)] public int MaxHealth;
        [Range(1, 5)] public int MaxWeaponsCount;
        [TextArea(2, 3)] public string Desctription;
    }
}