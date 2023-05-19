using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons.Projectiles;
using UnityEngine.Pool;

namespace Roguelike.Infrastructure.Factory
{
    public interface IProjectileFactory : IService
    {
        Projectile CreateProjectile(ProjectileType type, IObjectPool<Projectile> pool);
    }
}