using UnityEngine;

namespace Roguelike.StaticData.Characters
{
    [CreateAssetMenu(fileName = "New CharacterStaticData", menuName = "Static Data/Character")]
    public class CharacterStaticData : ScriptableObject
    {
        public CharacterId Id;
        public GameObject CharacterPrefab;
        [Range(1, 10)] public int MaxHealth;
    }
}