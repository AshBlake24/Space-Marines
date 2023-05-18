using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    public abstract class ProjectileStaticData : ScriptableObject
    {
        [Header("Stats")]
        [SerializeField] public WeaponId Id;
        [SerializeField] public ProjectileType Type;
        [SerializeField] public GameObject Prefab;
        [SerializeField] [Range(1, 1000)] public int Damage;
        [SerializeField] [Range(1, 100)] public float Speed;
        [SerializeField] [Range(0.1f, 3f)] public float Lifetime;
        
        [Header("View")]
        [SerializeField] public ParticleSystem MuzzleFlashVFX;
        [SerializeField] public ParticleSystem ProjectileVFX;
        [SerializeField] public ParticleSystem ImpactVFX;
    }
}