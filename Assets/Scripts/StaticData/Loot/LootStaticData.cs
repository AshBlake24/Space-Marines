using UnityEngine;

namespace Roguelike.StaticData.Loot
{
    [CreateAssetMenu(fileName = "New Loot", menuName = "Static Data/Loot/Loot Item")]
    public class LootStaticData : ScriptableObject
    {
        public LootId Id;
        public GameObject LootPrefab;
        [Range(0f, 100f)] public float DropChance;
    }
}