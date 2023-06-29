using Roguelike.Infrastructure.Services;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Weapons;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface ILootFactory : IService
    {
        GameObject CreateRandomWeapon(Vector3 position, RarityId minimalRarity = RarityId.Common);
        GameObject CreateConcreteWeapon(WeaponId weaponId, Vector3 position);
        void CreateRandomPowerup(Vector3 position);
        void CreateConcretePowerup(PowerupId powerupId, Vector3 position);
    }
}