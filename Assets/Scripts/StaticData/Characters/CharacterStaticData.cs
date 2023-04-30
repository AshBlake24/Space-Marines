using Roguelike.StaticData.Skills;
using UnityEngine;

namespace Roguelike.StaticData.Characters
{
    [CreateAssetMenu(fileName = "New CharacterStaticData", menuName = "Static Data/Character")]
    public class CharacterStaticData : ScriptableObject
    {
        public CharacterId Id;
        public Sprite Icon;
        public GameObject Prefab;
        public SkillStaticData Skill;
        [Range(1, 10)] public int MaxHealth;
        [TextArea(2, 3)] public string Desctription;
    }
}