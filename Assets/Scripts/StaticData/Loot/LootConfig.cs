using UnityEngine;

namespace Roguelike.StaticData.Loot
{
    public class LootConfig
    {
        public LootId Id;
        [Range(0f, 100f)] public float DropChance;
    }
}