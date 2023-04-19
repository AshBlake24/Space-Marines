using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Projectiles;
using Roguelike.Weapons.Projectiles;
using UnityEngine.Pool;

namespace Roguelike.Infrastructure.Factory
{
    public interface IProjectileFactory : IService
    {
        Projectile CreateProjectile(ProjectileId id, IObjectPool<Projectile> pool);
    }
}