using System;
using Roguelike.Logic;
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
        [SerializeField] protected VFX ImpactVFX;
        [SerializeField] protected VFX ProjectileVFX;

        public abstract void Construct<TStats>(TStats stats, ObjectPool<Projectile> projectilePool);
        public abstract void Init();
    }
}