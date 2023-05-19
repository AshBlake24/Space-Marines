using UnityEngine;

namespace Roguelike.StaticData.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Static Data/New enemy")]

    public class EnemyStaticData : ScriptableObject
    {
        [Header ("Stats")]
        public EnemyId Id;
        public GameObject Prefab;
        public float Danger;
        public int Health;
        public int Damage;
        public float Speed;
        public float AttackCooldown;
        public float AttackSpeed;

        [Header("Range enemy stats")]
        public int BulletInBurst;

        [Header("Taran enemy stats")]
        public float ChargeSpeedMultiplication;
    }
}
