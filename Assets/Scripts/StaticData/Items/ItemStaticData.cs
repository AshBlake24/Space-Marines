using Roguelike.StaticData.Enemies;
using UnityEngine;

namespace Roguelike.StaticData.Items
{

    [CreateAssetMenu(fileName = "New item", menuName = "Static Data/New item")]

    public class ItemStaticData : ScriptableObject
    {
        [Header("Stats")]
        public ItemId Id;
        public GameObject Prefab;
        public string Name;
    }
}
