using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Projectiles;
using Roguelike.Utilities;
using Roguelike.Weapons.Projectiles;

namespace Roguelike.Infrastructure.Factory
{
    public interface IProjectileFactory : IService
    {
        Projectile CreateProjectile(ProjectileId id, ObjectPool<Projectile> pool);
    }
}