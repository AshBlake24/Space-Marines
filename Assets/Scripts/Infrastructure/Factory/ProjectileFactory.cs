using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Projectiles;
using Roguelike.Weapons.Projectiles;
using Roguelike.Weapons.Projectiles.Stats;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Roguelike.Infrastructure.Factory
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly IStaticDataService _staticDataService;

        public ProjectileFactory(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public Projectile CreateProjectile(ProjectileId id, IObjectPool<Projectile> pool)
        {
            ProjectileStaticData projectileData = _staticDataService.GetProjectileData(id);

            return ConstructProjectile(projectileData, pool);
        }

        private Projectile ConstructProjectile(ProjectileStaticData projectileData, IObjectPool<Projectile> pool)
        {
            return projectileData.Type switch
            {
                ProjectileType.Bullet => CreateBullet(projectileData as BulletStaticData, pool),
                ProjectileType.Exploding => throw new NotImplementedException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Projectile CreateBullet(BulletStaticData projectileData, IObjectPool<Projectile> pool)
        {
            Bullet bullet = Object.Instantiate(projectileData.Prefab).GetComponent<Bullet>();

            bullet.Construct(InitializeBulletStats(projectileData), pool);

            return bullet;
        }

        private BulletStats InitializeBulletStats(BulletStaticData projectileData) => 
            new BulletStats(projectileData);
    }
}