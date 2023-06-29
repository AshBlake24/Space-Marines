using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Projectiles;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Enemies.EnemyStates
{
    public class RangeAttackState : EnemyState
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private ProjectileStaticData _bullet;
        [SerializeField] private List<Transform> _shotPoints;
        [SerializeField] private Transform _point;

        private Transform _shotPoint;
        private IProjectileFactory _factory;
        private IObjectPool<Projectile> _projectilesPool;

        public event Action NeedReloaded;
        public event Action Fired;

        private void Update()
        {
                LookAtPlayer();
        }

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            LookAtPlayer();

            _factory = AllServices.Container.Single<IProjectileFactory>();

            if (_projectilesPool == null)
                CreateProjectilesPool();

            animator.PlayAttack();
        }

        public void Shoot()
        {
            if (enemy == null)
                return;

            if (enemy.BulletInBurst > 0)
            {
                foreach (var point in _shotPoints)
                {
                    _shotPoint = point;
                    _projectilesPool.Get();
                }

                enemy.RangeAttack();
                Fired?.Invoke();
            }
            else
            {
                NeedReloaded?.Invoke();
            }
        }

        private void LookAtPlayer() =>
            transform.rotation = Quaternion.LookRotation(enemy.Target.transform.position - transform.position);


        private void CreateProjectilesPool()
        {
            _projectilesPool = new ObjectPool<Projectile>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false);
        }

        private Projectile CreatePoolItem()
        {
            Projectile bullet = GetProjectile();
            bullet.transform.SetParent(Helpers.GetPoolsContainer(gameObject.name));

            return bullet;
        }

        private Projectile GetProjectile()
        {
            return _factory.CreateProjectile(_bullet.Id, _projectilesPool);
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            projectile.transform.SetPositionAndRotation(_shotPoint.position, _shotPoint.rotation);
            projectile.ClearVFX();
            projectile.gameObject.SetActive(true);
            projectile.Init(enemy.Damage, _projectileSpeed);
        }

        private void OnReleaseToPool(Projectile bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile bullet) =>
            Destroy(bullet.gameObject);
    }
}
