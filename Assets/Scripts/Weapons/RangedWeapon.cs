using System;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Random;
using Roguelike.StaticData.Enhancements;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using Roguelike.Weapons.Stats;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Weapons
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private Transform _firePoint;

        private IRandomService _random;
        private IProjectileFactory _projectileFactory;
        private IObjectPool<Projectile> _projectilesPool;
        private ParticleSystem _muzzleFlashVFX;
        private RangedWeaponStats _stats;
        private int _totalDamage;
        
        public event Action Fired;

        public override WeaponStats Stats => _stats;
        public AmmoData AmmoData { get; private set; }

        public void Construct(RangedWeaponStats stats, IProjectileFactory projectileFactory, IRandomService randomService)
        {
            _stats = stats;
            _random = randomService;
            _projectileFactory = projectileFactory;
            _totalDamage = stats.Damage;
            AmmoData = new AmmoData(infinityAmmo: false, stats.MaxAmmo, stats.MaxAmmo);

            CreateProjectilesPool();
            CreateMuzzleFlashVFX();
        }

        public override void WriteProgress(PlayerProgress progress) => 
            progress.PlayerWeapons.SaveRangedWeapon(_stats.ID, AmmoData);

        public override void ReadProgress(PlayerProgress progress)
        {
            AmmoData = TryGetAmmoData(progress);
            TryApplyDamageEnhancement(progress);
        }

        public bool TryReload(float ammoAmountMultiplier) => 
            AmmoData.Reload(ammoAmountMultiplier);

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
            _projectileFactory.CreateProjectile(_stats.ProjectileData.Id, _projectilesPool);

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
            projectile.ClearVFX();
            projectile.Init(_totalDamage, _stats.ProjectileStartSpeed);
        }

        private void OnReleaseToPool(Projectile projectile) => 
            projectile.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile projectile) => 
            Destroy(projectile.gameObject);

        private AmmoData TryGetAmmoData(PlayerProgress progress) => 
            progress.PlayerWeapons.RangedWeaponsData.Find(weapon => weapon.ID == Stats.ID)?.AmmoData;

        private void TryApplyDamageEnhancement(PlayerProgress progress)
        {
            EnhancementData damageEnhancement = progress.State.Enhancements
                .SingleOrDefault(enhancement => enhancement.Id == EnhancementId.Damage);

            if (damageEnhancement is {Value: > 0})
            {
                int additiveDamage = _stats.Damage * damageEnhancement.Value / 100;
                _totalDamage = _stats.Damage + additiveDamage;
                Debug.Log($"Calculating\tBase Damage: {_stats.Damage}    Total Damage: {_totalDamage}");
            }
        }
    }
}