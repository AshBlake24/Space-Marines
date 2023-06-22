using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.StaticData;
using Roguelike.StaticData.Audio;
using Roguelike.StaticData.Player;
using Roguelike.StaticData.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private readonly Dictionary<Type, Dictionary<Enum, IStaticData>> _data = new();

        public PlayerStaticData Player { get; private set; }
        public GameConfig GameConfig { get; private set; }

        public void Load() =>
            LoadAllStaticData();

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
        
        public IEnumerable<TData> GetAllDataByType<TEnum, TData>() 
            where TEnum : Enum 
            where TData : IStaticData =>
            _data.ContainsKey(typeof(TEnum)) 
                ? _data[typeof(TEnum)].Select(pair => (TData) pair.Value)
                : null;

        private void LoadData()
        {
            Dictionary<Enum, IStaticData> data = Resources
                .LoadAll<ScriptableObject>(AssetPath.StaticDataPath)
                .OfType<IStaticData>()
                .ToDictionary(staticData => staticData.Key);

            foreach ((Enum key, IStaticData staticData) in data)
            {
                Type type = key.GetType();
                
                if (_data.ContainsKey(type))
                    _data[type].Add(key, staticData);
                else
                    _data[type] = new Dictionary<Enum, IStaticData>() {{key, staticData}};
            }
        }

        private void LoadAllStaticData()
        {
            LoadData();
            LoadWindows();
            LoadMusic();
            LoadPlayer();
            LoadGameConfig();
        }

        private void LoadPlayer() =>
            Player = Resources.Load<PlayerStaticData>(AssetPath.PlayerStaticDataPath);

        private void LoadGameConfig() =>
            GameConfig = Resources.Load<GameConfig>(AssetPath.GameConfigPath);

        private void LoadWindows()
        {
            Dictionary<Enum, IStaticData> data = Resources.Load<WindowStaticData>(AssetPath.WindowsStaticDataPath)
                .Configs
                .ToDictionary(config => config.WindowId as Enum, config => config as IStaticData);

            _data.Add(typeof(WindowId), data);
        }
        
        private void LoadMusic()
        {
            Dictionary<Enum, IStaticData> data = Resources.Load<MusicStaticData>(AssetPath.MusicStaticDataPath)
                .Configs
                .ToDictionary(config => config.LevelId as Enum, config => config as IStaticData);

            _data.Add(typeof(MusicId), data);
        }
    }
}