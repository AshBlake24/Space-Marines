using Roguelike.StaticData.Player;
using Roguelike.StaticData.Weapons;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData Player { get; }
        void LoadPlayer();
        void LoadWeapons();
        WeaponStaticData GetWeaponData(WeaponId id);
    }
}