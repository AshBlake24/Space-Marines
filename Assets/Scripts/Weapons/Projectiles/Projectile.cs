using System;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;
        
        [Header("Visual Settings")]
        [SerializeField] protected GameObject ImpactVFX;
        [SerializeField] protected GameObject IrojectileVFX;
        [SerializeField] protected GameObject MuzzleVFX;
        
        public abstract ProjectileStats Stats { get; }
        public abstract void Init();
    }
}