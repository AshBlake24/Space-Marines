using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Weapons;
using Roguelike.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IWeaponFactory : IService
    {
        IWeapon CreateWeapon(WeaponId id);
        IWeapon CreateWeapon(WeaponId id, Transform parent);
    }
}