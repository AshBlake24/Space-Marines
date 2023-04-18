using System;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;
        
        [Header("VFX")]
        [SerializeField] protected GameObject ImpactVFX;
        [SerializeField] protected GameObject ProjectileVFX;

        public abstract void Construct<TStats>(TStats stats, ObjectPool<Projectile> pool);
        public abstract void Init();
    }
}