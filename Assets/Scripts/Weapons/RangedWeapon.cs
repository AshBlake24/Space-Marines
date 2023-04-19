using System;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Pools;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using Roguelike.Weapons.Stats;
using UnityEngine;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon, IProgressWriter
    {
        [SerializeField] private Transform _firePoint;

        private IProjectileFactory _factory;
        private IParticlesPoolService _particlesPool;
        private RangedWeaponStats _stats;
        private ObjectPool<Projectile> _projectilesPool;
        private string _muzzleFlashKey;

        public override WeaponStats Stats => _stats;
        public int CurrentAmmo { get; private set; }
        public bool InfinityAmmo { get; private set; }

        private void Awake()
        {
            _factory = AllServices.Container.Single<IProjectileFactory>();
            _particlesPool = AllServices.Container.Single<IParticlesPoolService>();
        }

        public void Construct(RangedWeaponStats stats)
        {
            _stats = stats;
            InfinityAmmo = stats.InfinityAmmo;
            CurrentAmmo = stats.MaxAmmo;

            _projectilesPool = new ObjectPool<Projectile>(_stats.ProjectileData.Prefab);
            
            _muzzleFlashKey = _stats.ProjectileData.MuzzleFlashVFX.gameObject.name;
            _particlesPool.CreateNewPool(_muzzleFlashKey, _stats.ProjectileData.MuzzleFlashVFX);
        }

        public void ReadProgress(PlayerProgress progress)
        {
            AmmoData ammoData = progress.PlayerWeapons.Ammo.Find(weapon => weapon.ID == Stats.ID);

            if (ammoData != null)
            {
                InfinityAmmo = ammoData.InfinityAmmo;
                CurrentAmmo = ammoData.CurrentAmmo;
            }
        }

        public void WriteProgress(PlayerProgress progress) =>
            progress.PlayerWeapons.SaveRangedWeapon(Stats.ID, InfinityAmmo, CurrentAmmo);

        public override bool TryAttack()
        {
            if (InfinityAmmo)
            {
                Shot();
                Debug.Log($"{_stats.Name} shot! Bullets amount: Infinity");

                return true;
            }

            if (CurrentAmmo > 0)
            {
                Shot();
                CurrentAmmo--;
                Debug.Log($"{_stats.Name} shot! Bullets amount: {CurrentAmmo}");

                return true;
            }

            Debug.Log("Not enough ammo");

            return false;
        }

        private void Shot()
        {
            Projectile projectile = _projectilesPool.HasObjects
                ? _projectilesPool.GetInstance()
                : GetProjectile();

            projectile.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
            projectile.gameObject.SetActive(true);
            projectile.Init();

            SpawnMuzzleFlashVFX();
        }

        private void SpawnMuzzleFlashVFX()
        {
            ParticleSystem particles = _particlesPool.GetInstance(_muzzleFlashKey);
            particles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            particles.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
            //particles.transform.SetParent(_firePoint);
            particles.gameObject.SetActive(true);
            particles.Play();
        }

        private Projectile GetProjectile() =>
            _factory.CreateProjectile(_stats.ProjectileData.Id, _projectilesPool);
    }
}