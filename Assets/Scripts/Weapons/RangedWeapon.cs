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

        public override WeaponStats Stats => _stats;
        public int CurrentAmmo { get; private set; }
        public bool InfinityAmmo { get; private set; }

        private void Awake()
        {
            _random = AllServices.Container.Single<IRandomService>();
            _factory = AllServices.Container.Single<IProjectileFactory>();
        }

        public void Construct(RangedWeaponStats stats)
        {
            _stats = stats;
            InfinityAmmo = stats.InfinityAmmo;
            CurrentAmmo = stats.MaxAmmo;
            
            CreateProjectilesPool();
            CreateMuzzleFlashVFX(stats);
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
            _projectilesPool.Get();
            SpawnMuzzleFlashVFX();
        }
        
        private void CreateMuzzleFlashVFX(RangedWeaponStats stats)
        {
            _muzzleFlashVFX = Instantiate(
                stats.ProjectileData.MuzzleFlashVFX,
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

        private void SpawnMuzzleFlashVFX() => 
            _muzzleFlashVFX.Play();

        private Vector3 GetSpread() =>
            new Vector3(_random.Next(-_stats.Spread, _stats.Spread), 0, 0);

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