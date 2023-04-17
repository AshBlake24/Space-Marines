using System;
using System.Collections;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
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
        private RangedWeaponStats _stats;
        private ObjectPool<Projectile> _pool;
        private bool _isReloading;

        public override WeaponStats Stats => _stats;
        public int CurrentAmmo { get; private set; }
        public int CurrentClipAmmo { get; private set; }
        private bool CanReload => (CurrentClipAmmo < _stats.ClipSize) && (CurrentAmmo > 0);

        private void Awake()
        {
            _factory = AllServices.Container.Single<IProjectileFactory>();
        }

        public void Construct(RangedWeaponStats stats)
        {
            _stats = stats;
            CurrentAmmo = stats.MaxAmmo;
            CurrentClipAmmo = stats.ClipSize;

            InitializePool();
        }

        public void ReadProgress(PlayerProgress progress)
        {
            AmmoData ammoData = progress.PlayerWeapons.Ammo.Find(weapon => weapon.ID == Stats.ID);

            if (ammoData != null)
            {
                CurrentAmmo = ammoData.CurrentAmmo;
                CurrentClipAmmo = ammoData.CurrentClipAmmo;
            }
        }

        public void WriteProgress(PlayerProgress progress) =>
            progress.PlayerWeapons.SaveRangedWeapon(Stats.ID, CurrentAmmo, CurrentClipAmmo);

        public override bool TryAttack()
        {
            if (_isReloading)
                return false;

            if (CurrentClipAmmo > 0)
            {
                Shot();

                return true;
            }

            if (CanReload)
                StartCoroutine(Reloading());
            else
                Debug.Log("Not enough ammo");

            return false;
        }

        private void Shot()
        {
            Projectile projectile = _pool.HasObjects 
                ? _pool.GetInstance() 
                : GetProjectile();

            projectile.transform.SetPositionAndRotation(_firePoint.position, _firePoint.rotation);
            projectile.gameObject.SetActive(true);
            projectile.Init();
            
            CurrentClipAmmo--;

            Debug.Log($"{_stats.Name} shot! Bullets in magazine: {CurrentClipAmmo}. Bullets amount: {CurrentAmmo}");
        }

        private IEnumerator Reloading()
        {
            Debug.Log("Reloading!");
            _isReloading = true;

            yield return Helpers.GetTime(_stats.ReloadTime);

            int maxReloadAmount = Mathf.Min(_stats.ClipSize, CurrentAmmo);
            int availableBulletsInCurrentClip = _stats.ClipSize - CurrentClipAmmo;
            int reloadAmount = Mathf.Min(maxReloadAmount, availableBulletsInCurrentClip);
            CurrentClipAmmo += reloadAmount;
            CurrentAmmo -= reloadAmount;

            Debug.Log($"{_stats.Name} reloaded. Bullets in magazine: {CurrentClipAmmo}. Bullets amount: {CurrentAmmo}");
            _isReloading = false;
        }

        private void InitializePool()
        {
            Projectile projectile = GetProjectile();
            _pool = new ObjectPool<Projectile>(projectile.gameObject);
            _pool.AddInstance(projectile);
        }

        private Projectile GetProjectile() =>
            _factory.CreateProjectile(_stats.ProjectileData.Id);
    }
}