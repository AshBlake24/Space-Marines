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
        [Range(0.01f, 1f)] public float Danger;
        public int Health;
        public int Damage;
        public float Speed;
        public float AttackCooldown;
        public float AttackSpeed;
        public int BulletInBurst;
    }
}
