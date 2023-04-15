using Roguelike.StaticData.Weapons;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadWeapons();
        WeaponStaticData GetWeaponData(WeaponId id);
    }
}