using UnityEngine;

namespace Roguelike.Enemy
{
    [CreateAssetMenu(fileName = "New enemy", menuName = "Create enemy stats", order = 51)]

    public class EnemyStat : ScriptableObject
    {
        [SerializeField] private float _health;
        [SerializeField] private float _damage;

        public float Health => _health;
        public float Damage => _damage;
    }
}
