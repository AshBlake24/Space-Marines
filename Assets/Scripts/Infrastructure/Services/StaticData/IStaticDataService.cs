using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Enemies;
using Roguelike.StaticData.Levels;
using Roguelike.StaticData.Weapons.PickupableWeapons;
using Roguelike.StaticData.Windows;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData Player { get; }
        GameConfig GameConfig { get; }
        WeaponStaticData GetWeaponData(WeaponId id);
        ProjectileStaticData GetProjectileData(ProjectileId id);
        CharacterStaticData GetCharacterData(CharacterId id);
        SkillStaticData GetSkillStaticData(SkillId id);
        WindowConfig GetWindowConfig(WindowId id);
        EnemyStaticData GetEnemyStaticData(EnemyId id);
        LevelStaticData GetLevelStaticData(StageId id);
        PickupableWeaponsConfig GetPickupableWeaponConfig(WeaponId id);
        void Load();
    }
}