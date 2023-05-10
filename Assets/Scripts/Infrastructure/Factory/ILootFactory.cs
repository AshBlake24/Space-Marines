using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        void CreatePowerup(Vector3 position);
        GameObject CreateRandomWeapon(Vector3 position);
        GameObject CreateConcreteWeapon(WeaponId weaponId, Vector3 position);
    }
}