using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    public abstract class ProjectileStaticData : ScriptableObject
    {
        [Header("Stats")]
        public ProjectileId Id;
        public ProjectileType Type;
        public GameObject Prefab;
        public int Damage;
        [Range(1, 100)]public float Speed;
        [Range(0.1f, 3f)]public float Lifetime;
        
        [Header("View")]
        public ParticleSystem MuzzleFlashVFX;
        public ParticleSystem ProjectileVFX;
        public ParticleSystem ImpactVFX;
    }
}