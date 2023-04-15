using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;

namespace Roguelike.Infrastructure.Factory
{
    public interface IWeaponFactory : IService
    {
        IWeapon CreateWeapon(WeaponId id);
    }
}