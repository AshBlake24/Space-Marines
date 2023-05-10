using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Enemies;
using Roguelike.StaticData.Levels;
using Roguelike.StaticData.Loot;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Windows;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        PlayerStaticData Player { get; }
        GameConfig GameConfig { get; }
        PowerupDropTable PowerupDropTable { get; }
        IReadOnlyDictionary<WeaponId, int> WeaponsDropWeights { get; }
        WeaponStaticData GetWeaponData(WeaponId id);
        ProjectileStaticData GetProjectileData(ProjectileId id);
        CharacterStaticData GetCharacterData(CharacterId id);
        SkillStaticData GetSkillData(SkillId id);
        WindowConfig GetWindowConfig(WindowId id);
        EnemyStaticData GetEnemyData(EnemyId id);
        LevelStaticData GetLevelData(StageId id);
        PowerupStaticData GetPowerupData(PowerupId id);
        RarityStaticData GetRarityData(RarityId id);
        void Load();
    }
}