using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";
        
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentDataService _progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentDataService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in _gameFactory.ProgressWriters)
                progressWriter.WriteProgress(_progressService.PlayerProgress);
            
            PlayerPrefs.SetString(PlayerProgressKey, _progressService.PlayerProgress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(PlayerProgressKey)
                ?.FromJson<PlayerProgress>();
    }
}