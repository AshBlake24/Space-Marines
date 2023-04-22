using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Pools;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;

        protected float AccumulatedTime;
        protected ParticleSystem ProjectileVFX;
        protected IParticlesPoolService ParticlesPool;
        private IObjectPool<Projectile> _projectilesPool;

        private void Awake() => 
            ParticlesPool = AllServices.Container.Single<IParticlesPoolService>();

        private void Update() => 
            LifetimeTick();

        public abstract void Init();
        protected abstract void LifetimeTick();
        protected abstract void CreateImpactVFXPool();
        protected abstract void CreateProjectileVFX();

        protected void SpawnVFX(string key)
        {
            ParticleSystem particles = ParticlesPool.GetInstance(key);
            particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            particles.Play();
        }
        
        protected void ReturnToPool()
        {
            Rigidbody.velocity = Vector3.zero;
            _projectilesPool.Release(this);
        }
        
        protected void StopProjectileVFX() => 
            ProjectileVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        
        public virtual void Construct<TStats>(TStats stats, IObjectPool<Projectile> projectilePool) => 
            InitializeProjectilesPool(projectilePool);

        private void InitializeProjectilesPool(IObjectPool<Projectile> bulletPool)
        {
            if (bulletPool != null)
                _projectilesPool = bulletPool;
            else
                throw new ArgumentNullException(nameof(bulletPool), "Projectiles pool cannot be null");
        }
    }
}