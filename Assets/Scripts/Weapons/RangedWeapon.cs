using System;
using System.Collections;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using Roguelike.Weapons.Stats;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon, IProgressWriter
    {
        [SerializeField] private Transform _firePoint;

        private IRandomService _random;
        private IProjectileFactory _factory;
        private IObjectPool<Projectile> _projectilesPool;
        private ParticleSystem _muzzleFlashVFX;
        private RangedWeaponStats _stats;
        
        public event Action Fired;

        public override WeaponStats Stats => _stats;
        public AmmoData AmmoData { get; private set; }

        private void Awake()
        {
            _random = AllServices.Container.Single<IRandomService>();
            _factory = AllServices.Container.Single<IProjectileFactory>();
        }

        public void Construct(RangedWeaponStats stats)
        {
            _stats = stats;
            AmmoData = new AmmoData(infinityAmmo: false, _stats.MaxAmmo, _stats.MaxAmmo);
            
            CreateProjectilesPool();
            CreateMuzzleFlashVFX();
        }

        public void WriteProgress(PlayerProgress progress) => 
            progress.PlayerWeapons.SaveRangedWeapon(_stats.ID, AmmoData);

        public void ReadProgress(PlayerProgress progress)
        {
            RangedWeaponData weaponData = progress.PlayerWeapons.RangedWeapons.Find(weapon => weapon.ID == Stats.ID);

            if (weaponData != null)
                AmmoData = weaponData.AmmoData;
        }

        public override bool TryAttack()
        {
            if (AmmoData.CurrentAmmo > 0 || AmmoData.InfinityAmmo)
            {
                Shot();
                return true;
            }
            
            return false;
        }

        private void Shot()
        {
            if (AmmoData.InfinityAmmo == false)
                AmmoData.CurrentAmmo--;

            for (int i = 0; i < _stats.BulletsPerShot; i++)
                _projectilesPool.Get();

            SpawnMuzzleFlashVFX();
            Fired?.Invoke();
        }

        private void CreateMuzzleFlashVFX()
        {
            _muzzleFlashVFX = Instantiate(
                _stats.ProjectileData.MuzzleFlashVFX,
                _firePoint.position,
                _firePoint.rotation,
                _firePoint);
            _muzzleFlashVFX.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void CreateProjectilesPool()
        {
            _projectilesPool = new ObjectPool<Projectile>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false);
        }

        private Vector3 GetSpread()
        {
            return new Vector3(
                _random.Next(-_stats.HorizontalSpread, _stats.HorizontalSpread), 
                _random.Next(-_stats.VerticalSpread, _stats.VerticalSpread),
                _random.Next(-_stats.HorizontalSpread, _stats.HorizontalSpread));
        }

        private void SpawnMuzzleFlashVFX() => 
            _muzzleFlashVFX.Play();

        private Projectile GetProjectile() =>
            _factory.CreateProjectile(_stats.ProjectileData.Id, _projectilesPool);

        private Projectile CreatePoolItem()
        {
            Projectile projectile = GetProjectile();
            projectile.transform.SetParent(Helpers.GetPoolsContainer(gameObject.name));

            return projectile;
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            projectile.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
            projectile.transform.forward += GetSpread();
            projectile.gameObject.SetActive(true);
            projectile.Init();
        }

        private void OnReleaseToPool(Projectile projectile) => 
            projectile.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile projectile) => 
            Destroy(projectile.gameObject);
    }
}