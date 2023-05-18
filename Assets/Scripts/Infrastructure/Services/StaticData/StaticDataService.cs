using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData;
using Roguelike.StaticData.Characters;
using Roguelike.StaticData.Enemies;
using Roguelike.StaticData.Levels;
using Roguelike.StaticData.Loot.Powerups;
using Roguelike.StaticData.Loot.Rarity;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Projectiles;
using Roguelike.StaticData.Skills;
using Roguelike.StaticData.Weapons;
using Roguelike.StaticData.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WeaponId, WeaponStaticData> _weapons;
        private Dictionary<WeaponId, int> _weaponsDropWeights;
        private Dictionary<WeaponId, ProjectileStaticData> _projectiles;
        private Dictionary<CharacterId, CharacterStaticData> _characters;
        private Dictionary<SkillId, SkillStaticData> _skills;
        private Dictionary<WindowId, WindowConfig> _windows;
        private Dictionary<EnemyId, EnemyStaticData> _enemies;
        private Dictionary<StageId, LevelStaticData> _levels;
        private Dictionary<PowerupId, PowerupStaticData> _powerups;
        private Dictionary<RarityId, RarityStaticData> _rarity;

        public PlayerStaticData Player { get; private set; }
        public GameConfig GameConfig { get; private set; }
        public PowerupDropTable PowerupDropTable { get; private set; }
        public IReadOnlyDictionary<WeaponId, int> WeaponsDropWeights => _weaponsDropWeights;

        public void Load()
        {
            LoadWeapons();
            LoadProjectiles();
            LoadCharacters();
            LoadSkills();
            LoadWindows();
            LoadEnemies();
            LoadLevels();
            LoadPlayer();
            LoadRarity();
            LoadPowerups();
            LoadGameConfig();
            LoadPowerupDropTable();
            LoadWeaponsDropWeights();
        }

        private void LoadWeaponsDropWeights()
        {
            _weaponsDropWeights = new Dictionary<WeaponId, int>();

            foreach (WeaponStaticData weaponData in _weapons.Values)
            {
                RarityStaticData rarityData = GetRarityData(weaponData.Rarity);
                _weaponsDropWeights.Add(weaponData.Id, rarityData.Weight);
            }
        }

        public WeaponStaticData GetWeaponData(WeaponId id) =>
            _weapons.TryGetValue(id, out WeaponStaticData staticData)
                ? staticData
                : null;

        public ProjectileStaticData GetProjectileData(WeaponId id) =>
            _projectiles.TryGetValue(id, out ProjectileStaticData staticData)
                ? staticData
                : null;

        public CharacterStaticData GetCharacterData(CharacterId id) =>
            _characters.TryGetValue(id, out CharacterStaticData staticData)
                ? staticData
                : null;

        public SkillStaticData GetSkillData(SkillId id) =>
            _skills.TryGetValue(id, out SkillStaticData staticData)
                ? staticData
                : null;

        public EnemyStaticData GetEnemyData(EnemyId id) =>
            _enemies.TryGetValue(id, out EnemyStaticData staticData)
                ? staticData
                : null;

        public WindowConfig GetWindowConfig(WindowId id) =>
            _windows.TryGetValue(id, out WindowConfig windowConfig)
                ? windowConfig
                : null;

        public LevelStaticData GetLevelData(StageId id) =>
            _levels.TryGetValue(id, out LevelStaticData staticData)
                ? staticData
                : null;

        public PowerupStaticData GetPowerupData(PowerupId id) =>
            _powerups.TryGetValue(id, out PowerupStaticData staticData)
                ? staticData
                : null;

        public RarityStaticData GetRarityData(RarityId id) =>
            _rarity.TryGetValue(id, out RarityStaticData staticData)
                ? staticData
                : null;

        private void LoadWeapons() =>
            _weapons = Resources.LoadAll<WeaponStaticData>(AssetPath.WeaponsStaticDataPath)
                .ToDictionary(weapon => weapon.Id);
        
        private void LoadProjectiles() =>
            _projectiles = Resources.LoadAll<ProjectileStaticData>(AssetPath.ProjectilesStaticDataPath)
                .ToDictionary(projectile => projectile.Id);

        private void LoadCharacters() =>
            _characters = Resources.LoadAll<CharacterStaticData>(AssetPath.CharactersStaticDataPath)
                .ToDictionary(character => character.Id);

        private void LoadSkills() =>
            _skills = Resources.LoadAll<SkillStaticData>(AssetPath.SkillsStaticDataPath)
                .ToDictionary(skill => skill.Id);

        private void LoadEnemies() =>
            _enemies = Resources.LoadAll<EnemyStaticData>(AssetPath.EnemiesPath)
                .ToDictionary(enemy => enemy.Id);

        private void LoadLevels() =>
            _levels = Resources.LoadAll<LevelStaticData>(AssetPath.LevelsPath)
                .ToDictionary(level => level.Id);

        private void LoadPowerups() =>
            _powerups = Resources.LoadAll<PowerupStaticData>(AssetPath.PowerupStaticDataPath)
                .ToDictionary(powerup => powerup.Id);

        private void LoadWindows() =>
            _windows = Resources.Load<WindowStaticData>(AssetPath.WindowsStaticDataPath)
                .Configs
                .ToDictionary(config => config.WindowId, x => x);

        private void LoadRarity() =>
            _rarity = Resources.LoadAll<RarityStaticData>(AssetPath.RarityStaticDataPath)
                .ToDictionary(rarity => rarity.Id);

        private void LoadPlayer() =>
            Player = Resources.Load<PlayerStaticData>(AssetPath.PlayerStaticDataPath);

        private void LoadGameConfig() =>
            GameConfig = Resources.Load<GameConfig>(AssetPath.GameConfigPath);

        private void LoadPowerupDropTable() => 
            PowerupDropTable = Resources.Load<PowerupDropTable>(AssetPath.PowerupDropTablePath);
    }
}