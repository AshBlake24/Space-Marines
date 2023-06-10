using System;
using Roguelike.Audio.Factory;
using Roguelike.Audio.Logic;
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
        private readonly IAudioFactory _audioFactory;

        public ProjectileFactory(IStaticDataService staticDataService, IAudioFactory audioFactory)
        {
            _staticDataService = staticDataService;
            _audioFactory = audioFactory;
        }

        public Projectile CreateProjectile(ProjectileId id, IObjectPool<Projectile> pool)
        {
            ProjectileStaticData projectileData = _staticDataService.GetDataById<ProjectileId, ProjectileStaticData>(id);

            return ConstructProjectile(projectileData, pool);
        }

        private Projectile ConstructProjectile(ProjectileStaticData projectileData, IObjectPool<Projectile> pool)
        {
            Projectile projectile = projectileData.Type switch
            {
                ProjectileType.Bullet => CreateBullet(projectileData as BulletStaticData, pool),
                ProjectileType.Exploding => CreateExplodingAmmo(projectileData as ExplodingProjectileStaticData, pool),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            projectile.GetComponent<ProjectileAudioPlayer>().Construct(_audioFactory, projectileData.Clip);

            return projectile;
        }

        private Projectile CreateBullet(BulletStaticData projectileData, IObjectPool<Projectile> pool)
        {
            Bullet bullet = Object.Instantiate(projectileData.Prefab).GetComponent<Bullet>();

            bullet.Construct(InitializeBulletStats(projectileData), pool);

            return bullet;
        }
        
        private Projectile CreateExplodingAmmo(ExplodingProjectileStaticData projectileData, IObjectPool<Projectile> pool)
        {
            ExplodingProjectile exploding = Object.Instantiate(projectileData.Prefab)
                .GetComponent<ExplodingProjectile>();

            exploding.Construct(InitializeExplodingProjectileStats(projectileData), pool);

            return exploding;
        }

        private ExplodingProjectileStats InitializeExplodingProjectileStats(ExplodingProjectileStaticData projectileData) => 
            new ExplodingProjectileStats(projectileData);

        private BulletStats InitializeBulletStats(BulletStaticData projectileData) => 
            new BulletStats(projectileData);
    }
}