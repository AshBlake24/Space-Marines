using System;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.StaticData.Projectiles
{
    public abstract class ProjectileStaticData : ScriptableObject, IStaticData
    {
        [Header("Stats")] 
        public ProjectileId Id;
        public ProjectileType Type;
        public GameObject Prefab;
        public float Lifetime;
        
        [Header("View")]
        public ParticleSystem MuzzleFlashVFX;
        public ParticleSystem ProjectileVFX;
        public ParticleSystem ImpactVFX;
        
        public Enum Key => Id;
    }
}