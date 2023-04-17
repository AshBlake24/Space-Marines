using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    public abstract class ProjectileStaticData : ScriptableObject
    {
        public ProjectileId Id;
        public ProjectileType Type;
        public GameObject Prefab;
        public int Damage;
        public float Speed;
    }
}