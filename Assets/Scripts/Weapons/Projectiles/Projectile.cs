using System;
using JetBrains.Annotations;
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

        protected int Damage;
        protected string ImpactVFXKey;
        private float _accumulatedTime;
        private IParticlesPoolService _particlesPool;
        private IObjectPool<Projectile> _projectilesPool;
        private ParticleSystem _projectileVFX;
        private TrailRenderer _trailRenderer;

        protected abstract ProjectileStats Stats { get; }

        private void Awake() =>
            _particlesPool = AllServices.Container.Single<IParticlesPoolService>();

        private void Update()
        {
            LifetimeTick();
        }

        public virtual void Construct<TStats>(TStats projectileStats, IObjectPool<Projectile> projectilePool)
        {
            InitializeProjectilesPool(projectilePool);
            CreateImpactVFXPool();
            CreateProjectileVFX();
        }

        public void Init(int damage, float startSpeed) =>
            InitProjectile(damage, startSpeed, transform.forward);

        public void Init(int damage, float startSpeed, Vector3 direction) =>
            InitProjectile(damage, startSpeed, direction);

        public void ClearVFX()
        {
            _projectileVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            
            if (_trailRenderer != null)
                _trailRenderer.Clear();
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

        private void InitProjectile(int damage, float startSpeed, Vector3 direction)
        {
            Damage = damage;
            Rigidbody.angularVelocity = Vector3.zero;
            Rigidbody.AddForce(direction * startSpeed, ForceMode.VelocityChange);
            _accumulatedTime = 0f;
            _projectileVFX.Play();
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
            _trailRenderer = _projectileVFX.GetComponentInChildren<TrailRenderer>();
            ClearVFX();
        }
    }
}