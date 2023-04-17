using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Projectiles;
using Roguelike.Weapons.Projectiles;
using Roguelike.Weapons.Projectiles.Stats;
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

        public Projectile CreateProjectile(ProjectileId id)
        {
            ProjectileStaticData projectileData = _staticDataService.GetProjectileData(id);

            return ConstructProjectile(projectileData);
        }

        private Projectile ConstructProjectile(ProjectileStaticData projectileData)
        {
            return projectileData.Type switch
            {
                ProjectileType.Bullet => CreateBullet(projectileData as BulletStaticData),
                ProjectileType.Shrapnel => CreateShrapnel(projectileData as ShrapnelStaticData),
                ProjectileType.Exploding => CreateExplodingProjectile(projectileData as ExplodingProjectileStaticData),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private Projectile CreateBullet(BulletStaticData projectileData)
        {
            Bullet bullet = Object.Instantiate(projectileData.Prefab).GetComponent<Bullet>();

            bullet.Construct(InitializeBulletStats(projectileData));

            return bullet;
        }

        private Projectile CreateShrapnel(ShrapnelStaticData projectileData)
        {
            Shrapnel shrapnel = Object.Instantiate(projectileData.Prefab).GetComponent<Shrapnel>();

            shrapnel.Construct(InitializeSrapnelStats(projectileData));

            return shrapnel;
        }

        private Projectile CreateExplodingProjectile(ExplodingProjectileStaticData projectileData)
        {
            ExplodingProjectile explodingProjectile =
                Object.Instantiate(projectileData.Prefab).GetComponent<ExplodingProjectile>();
            
            explodingProjectile.Construct(InitializeExplodingProjectileStats(projectileData));

            return explodingProjectile;
        }

        private BulletStats InitializeBulletStats(BulletStaticData projectileData) => 
            new BulletStats(projectileData);

        private ShrapnelStats InitializeSrapnelStats(ShrapnelStaticData projectileData) => 
            new ShrapnelStats(projectileData);

        private ExplodingProjectileStats InitializeExplodingProjectileStats(ExplodingProjectileStaticData projectileData) => 
            new ExplodingProjectileStats(projectileData);
    }
}