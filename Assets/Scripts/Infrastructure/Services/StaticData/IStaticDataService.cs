using Roguelike.StaticData.Player;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData Player { get; }
        void Load();
        WeaponStaticData GetWeaponData(WeaponId id);
        ProjectileStaticData GetProjectileData(ProjectileId id);
    }
}