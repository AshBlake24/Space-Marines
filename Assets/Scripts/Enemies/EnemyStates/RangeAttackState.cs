using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Roguelike.Enemies.Animators;
using Roguelike.StaticData.Projectiles;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Enemies.EnemyStates
{
    public class RangeAttackState : EnemyState
    {
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private ProjectileStaticData _bullet;
        [SerializeField] private Transform _shotPoint;

        private IProjectileFactory _factory;
        private IObjectPool<Projectile> _projectilesPool;

        public event Action NeedReloaded;

        private void Update()
        {
            LookAtPlayer();
        }

        public override void Enter(Enemy enemy, EnemyAnimator enemyAnimator)
        {
            base.Enter(enemy, enemyAnimator);

            LookAtPlayer();

            _factory = AllServices.Container.Single<IProjectileFactory>();

            CreateProjectilesPool();

            animator.PlayAttack();
        }

        public void Shoot()
        {
            if (enemy.BulletInBurst > 0)
            {
                _projectilesPool.Get();
                enemy.RangeAttack();
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
            projectile.gameObject.SetActive(true);
            Vector3 direction = (enemy.Target.transform.position - transform.position).normalized;
            projectile.ClearVFX();
            projectile.Init(enemy.Damage, _projectileSpeed, direction);
        }

        private void OnReleaseToPool(Projectile bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile bullet) =>
            Destroy(bullet.gameObject);
    }
}
