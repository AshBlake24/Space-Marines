using System;
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
        private readonly Dictionary<Type, Dictionary<Enum, IStaticData>> _data = new();

        private Dictionary<WeaponId, int> _weaponsDropWeights;
        
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
            LoadStages();
            LoadRegions();
            LoadRarity();
            LoadPowerups();

            LoadPlayer();
            LoadGameConfig();
            LoadPowerupDropTable();
            LoadWeaponsDropWeights();
        }

        public TResult GetDataById<TKey, TResult>(TKey id) 
            where TKey : Enum
            where TResult : IStaticData
        {
            KeyValuePair<Type, Dictionary<Enum, IStaticData>> data = 
                _data.SingleOrDefault(data => data.Key == typeof(TKey));

            if (data.Equals(default(KeyValuePair<Type, Dictionary<Enum, IStaticData>>)))
                throw new ArgumentNullException($"{typeof(TKey)}", "This data does not exist");

            return (TResult) data.Value.SingleOrDefault(staticData => Equals(staticData.Key, id)).Value;
        }
        
        private void LoadWeaponsDropWeights()
        {
            _weaponsDropWeights = new Dictionary<WeaponId, int>();

            IEnumerable<WeaponStaticData> data = 
                _data.Single(data => data.Key == typeof(WeaponId))
                    .Value
                    .Where(data => data.Value is WeaponStaticData)
                    .Select(data => (WeaponStaticData) data.Value);

            foreach (WeaponStaticData weaponData in data)
            {
                RarityStaticData rarityData = GetDataById<RarityId, RarityStaticData>(weaponData.Rarity);
                _weaponsDropWeights.Add(weaponData.Id, rarityData.Weight);
            }
        }

        private void LoadWeapons()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<WeaponStaticData>(AssetPath.WeaponsStaticDataPath)
                .ToDictionary(weapon => weapon.Id as Enum, weapon => weapon as IStaticData);
            
            _data.Add(typeof(WeaponId), data);
        }

        private void LoadProjectiles()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<ProjectileStaticData>(AssetPath.ProjectilesStaticDataPath)
                .ToDictionary(projectile => projectile.Id as Enum, projectile => projectile as IStaticData);
            
            _data.Add(typeof(ProjectileId), data);
        }

        private void LoadCharacters()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<CharacterStaticData>(AssetPath.CharactersStaticDataPath)
                .ToDictionary(character => character.Id as Enum, character => character as IStaticData);
            
            _data.Add(typeof(CharacterId), data);
        }

        private void LoadSkills()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<SkillStaticData>(AssetPath.SkillsStaticDataPath)
                .ToDictionary(skill => skill.Id as Enum, skill => skill as IStaticData);
            
            _data.Add(typeof(SkillId), data);
        }

        private void LoadEnemies()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<EnemyStaticData>(AssetPath.EnemiesPath)
                .ToDictionary(enemy => enemy.Id as Enum, enemy => enemy as IStaticData);
            
            _data.Add(typeof(EnemyId), data);
        }

        private void LoadStages()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<StageStaticData>(AssetPath.StagesPath)
                .ToDictionary(stage => stage.Id as Enum, stage => stage as IStaticData);
            
            _data.Add(typeof(StageId), data);
        }

        private void LoadRegions()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<RegionStaticData>(AssetPath.RegionsPath)
                .ToDictionary(region => region.Id as Enum, region => region as IStaticData);
            
            _data.Add(typeof(LevelId), data);
        }

        private void LoadPowerups()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<PowerupStaticData>(AssetPath.PowerupStaticDataPath)
                .ToDictionary(powerup => powerup.Id as Enum, powerup => powerup as IStaticData);
            
            _data.Add(typeof(PowerupId), data);
        }

        private void LoadWindows()
        {
            Dictionary<Enum, IStaticData> data = Resources.Load<WindowStaticData>(AssetPath.WindowsStaticDataPath)
                .Configs
                .ToDictionary(config => config.WindowId as Enum, config => config as IStaticData);
            
            _data.Add(typeof(WindowId), data);
        }

        private void LoadRarity()
        {
            Dictionary<Enum, IStaticData> data = Resources.LoadAll<RarityStaticData>(AssetPath.RarityStaticDataPath)
                .ToDictionary(rarity => rarity.Id as Enum, rarity => rarity as IStaticData);
            
            _data.Add(typeof(RarityId), data);
        }

        private void LoadPlayer() =>
            Player = Resources.Load<PlayerStaticData>(AssetPath.PlayerStaticDataPath);

        private void LoadGameConfig() =>
            GameConfig = Resources.Load<GameConfig>(AssetPath.GameConfigPath);

        private void LoadPowerupDropTable() => 
            PowerupDropTable = Resources.Load<PowerupDropTable>(AssetPath.PowerupDropTablePath);
    }
}