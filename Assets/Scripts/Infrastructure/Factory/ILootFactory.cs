using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        void CreatePowerup(Vector3 position);
        void CreateRandomWeapon(Transform at);
        void CreateConcreteWeapon(WeaponId weaponId, Vector3 position);
    }
}