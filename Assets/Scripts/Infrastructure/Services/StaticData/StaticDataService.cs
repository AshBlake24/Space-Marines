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
            LoadEnemies();
            LoadStages();
            LoadRegions();
            LoadRarity();
            LoadPowerups();
            LoadWindows();
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
        
        private void LoadData<TData>(string path) where TData : ScriptableObject, IStaticData
        {
            Dictionary<Enum, IStaticData> data = Resources
                .LoadAll<TData>(path)
                .ToDictionary(weapon => weapon.Key, weapon => weapon as IStaticData);

            if (data.Count > 0)
            {
                IStaticData staticData = data.Select(pair => pair.Value).First();
                _data.Add(staticData.Key.GetType(), data);
            }
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

        private void LoadWeapons() => LoadData<WeaponStaticData>(AssetPath.WeaponsStaticDataPath);
        private void LoadProjectiles() => LoadData<ProjectileStaticData>(AssetPath.ProjectilesStaticDataPath);
        private void LoadCharacters() => LoadData<CharacterStaticData>(AssetPath.CharactersStaticDataPath);
        private void LoadSkills() => LoadData<SkillStaticData>(AssetPath.SkillsStaticDataPath);
        private void LoadEnemies() => LoadData<EnemyStaticData>(AssetPath.EnemiesPath);
        private void LoadStages() => LoadData<StageStaticData>(AssetPath.StagesPath);
        private void LoadRegions() => LoadData<RegionStaticData>(AssetPath.RegionsPath);
        private void LoadPowerups() => LoadData<PowerupStaticData>(AssetPath.PowerupStaticDataPath);
        private void LoadRarity() => LoadData<RarityStaticData>(AssetPath.RarityStaticDataPath);
        
        private void LoadPlayer() =>
            Player = Resources.Load<PlayerStaticData>(AssetPath.PlayerStaticDataPath);
        
        private void LoadGameConfig() =>
            GameConfig = Resources.Load<GameConfig>(AssetPath.GameConfigPath);

        private void LoadPowerupDropTable() => 
            PowerupDropTable = Resources.Load<PowerupDropTable>(AssetPath.PowerupDropTablePath);

        private void LoadWindows()
        {
            Dictionary<Enum, IStaticData> data = Resources.Load<WindowStaticData>(AssetPath.WindowsStaticDataPath)
                .Configs
                .ToDictionary(config => config.WindowId as Enum, config => config as IStaticData);
            
            _data.Add(typeof(WindowId), data);
        }
    }
}