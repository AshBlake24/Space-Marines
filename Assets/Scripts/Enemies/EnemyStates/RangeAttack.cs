using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Projectiles;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Enemies.EnemyStates
{
    public class RangeAttack : EnemyState
    {
        [SerializeField] private int _damage;
        [SerializeField] private ProjectileStaticData _bullet;
        [SerializeField] private Transform _shotPoint;

        private IProjectileFactory _factory;
        private IObjectPool<Projectile> _projectilesPool;


        protected override void OnDisable()
        {
            StopCoroutine(Attack());

            base.OnDisable();
        }

        public override void Enter(Enemy enemy)
        {
            base.Enter(enemy);

            _factory = AllServices.Container.Single<IProjectileFactory>();

            CreateProjectilesPool();

            StartCoroutine(Attack());
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
            return _factory.CreateProjectile(_bullet.Id, _projectilesPool);
        }

        private void OnTakeFromPool(Projectile projectile)
        {
            projectile.transform.SetPositionAndRotation(_shotPoint.position, _shotPoint.rotation);
            projectile.gameObject.SetActive(true);
            Vector3 direction = (enemy.Target.transform.position - transform.position).normalized;
            projectile.Init(_damage, direction);
        }

        private void OnReleaseToPool(Projectile bullet) =>
            bullet.gameObject.SetActive(false);

        private void OnDestroyItem(Projectile bullet) =>
            Destroy(bullet.gameObject);

        private IEnumerator Attack()
        {
            while (!gameObject.IsDestroyed())
            {
                Vector3 relativePos = enemy.Target.transform.position - transform.position;

                transform.rotation = Quaternion.LookRotation(relativePos);
                _projectilesPool.Get();

                yield return Helpers.GetTime(0.5f);
            }
        }
    }
}
