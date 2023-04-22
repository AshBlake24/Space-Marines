using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Rigidbody Rigidbody;

        protected string ImpactVFXKey;
        private float _accumulatedTime;
        private ParticleSystem _projectileVFX;
        private IParticlesPoolService _particlesPool;
        private IObjectPool<Projectile> _projectilesPool;

        protected abstract ProjectileStats Stats { get; }

        private void Awake() => 
            _particlesPool = AllServices.Container.Single<IParticlesPoolService>();

        private void Update() => 
            LifetimeTick();

        public virtual void Construct<TStats>(TStats stats, IObjectPool<Projectile> projectilePool)
        {
            InitializeProjectilesPool(projectilePool);
            CreateImpactVFXPool();
            CreateProjectileVFX();
        }

        public void Init()
        {
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.velocity = transform.forward * Stats.Speed;
            _accumulatedTime = 0f;
            _projectileVFX.Play();
        }

        protected void SpawnVFX(string key)
        {
            ParticleSystem particles = _particlesPool.GetInstance(key);
            particles.transform.SetPositionAndRotation(transform.position, transform.rotation);
            particles.Play();
        }

        protected void ReturnToPool()
        {
            Rigidbody.velocity = Vector3.zero;
            _projectilesPool.Release(this);
        }

        private void LifetimeTick()
        {
            _accumulatedTime += Time.deltaTime;

            if (_accumulatedTime >= Stats.Lifetime)
                ReturnToPool();
        }

        private void InitializeProjectilesPool(IObjectPool<Projectile> bulletPool)
        {
            if (bulletPool != null)
                _projectilesPool = bulletPool;
            else
                throw new ArgumentNullException(nameof(bulletPool), "Projectiles pool cannot be null");
        }

        private void CreateImpactVFXPool()
        {
            ImpactVFXKey = Stats.ProjectileVFX.gameObject.name;
            _particlesPool.CreateNewPool(ImpactVFXKey, Stats.ImpactVFX);
        }

        private void CreateProjectileVFX()
        {
            _projectileVFX = Instantiate(Stats.ProjectileVFX, transform.position, transform.rotation, transform);
            StopProjectileVFX();
        }

        private void StopProjectileVFX() => 
            _projectileVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}