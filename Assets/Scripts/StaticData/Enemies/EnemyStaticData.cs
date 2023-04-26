using UnityEngine;

namespace Roguelike.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Static Data/New enemy")]

    public class EnemyStaticData : ScriptableObject
    {
        [Header ("Stats")]
        public EnemyId Id;
        public EnemyType EnemyType;
        public GameObject Prefab;
        public int Health;
        public int Damage;
        public float Speed;
    }
}
