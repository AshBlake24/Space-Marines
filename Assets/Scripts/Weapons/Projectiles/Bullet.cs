using System;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;

namespace Roguelike.Weapons.Projectiles
{
    public class Bullet : Projectile
    {
        private BulletStats _stats;
        private ObjectPool<Projectile> _pool;

        public override ProjectileStats Stats => _stats;

        public void Construct(BulletStats stats, ObjectPool<Projectile> pool)
        {
            _stats = stats;
            _pool = pool;
        }

        public override void Init()
        {
            Rigidbody.AddForce(transform.forward * Stats.Speed, ForceMode.VelocityChange);
            
            //MuzzleVFX = Instantiate(MuzzleVFX, transform.position, transform.rotation);
            //Destroy(MuzzleVFX, 1.5f);
        }
        private void FixedUpdate()
        {
            transform.rotation = Quaternion.LookRotation(Rigidbody.velocity); // Sets rotation to look at direction of movement
            
            
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}