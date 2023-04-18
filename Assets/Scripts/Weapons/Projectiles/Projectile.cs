using System;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        protected const float Lifetime = 1.5f;
        
        [SerializeField] protected Rigidbody Rigidbody;
        
        [Header("Visual Settings")]
        [SerializeField] protected GameObject ImpactVFX;
        [SerializeField] protected GameObject IrojectileVFX;
        [SerializeField] protected GameObject MuzzleVFX;

        public abstract void Construct<TStats>(TStats stats, ObjectPool<Projectile> pool);
        public abstract void Init();
    }
}