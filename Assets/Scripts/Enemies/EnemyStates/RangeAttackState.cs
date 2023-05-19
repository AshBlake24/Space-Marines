using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Projectiles;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Enemies.EnemyStates
{
    public class RangeAttackState : EnemyState
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private ProjectileStaticData _bullet;
        [SerializeField] private Transform _shotPoint;

        private IProjectileFactory _factory;
        private Coroutine _coroutine;
        private IObjectPool<Projectile> _projectilesPool;

        public event Action NeedReloaded;


        protected override void OnDisable()
        {
            StopCoroutine(_coroutine);

            base.OnDisable();
        }

        public override void Enter(Enemy enemy)
        {
            base.Enter(enemy);

            _factory = AllServices.Container.Single<IProjectileFactory>();

            CreateProjectilesPool();

            _coroutine = StartCoroutine(Attack());
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

        private Projectile CreatePoolItem()
        {
            Projectile bullet = GetProjectile();
            bullet.transform.SetParent(Helpers.GetPoolsContainer(gameObject.name));

            return bullet;
        }

        private Projectile GetProjectile()
        {
            return _factory.CreateProjectile(_bullet.Type, _projectilesPool);
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            projectile.transform.SetPositionAndRotation(_shotPoint.position, _shotPoint.rotation);
            projectile.gameObject.SetActive(true);
            Vector3 direction = (enemy.Target.transform.position - transform.position).normalized;
            projectile.ClearVFX();
            projectile.Init(_damage, _projectileSpeed, direction);
        }

        private void OnReleaseToPool(Projectile bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile bullet) =>
            Destroy(bullet.gameObject);

        private IEnumerator Attack()
        {
            while (enemy.BulletInBurst > 0)
            {
                Vector3 relativePos = enemy.Target.transform.position - transform.position;

                transform.rotation = Quaternion.LookRotation(relativePos);
                _projectilesPool.Get();

                enemy.RangeAttack();

                yield return Helpers.GetTime(enemy.AttackSpeed);
            }

            NeedReloaded?.Invoke();
        }
    }
}
