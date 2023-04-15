using System.Collections.Generic;
using System.IO;
using Roguelike.Data;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";
        
        private readonly IPersistentDataService _progressService;
        private readonly List<IProgressReader> _progressReaders;
        private readonly List<IProgressWriter> _progressWriters;
        
        public SaveLoadService(IPersistentDataService progressService)
        {
            _progressService = progressService;
            _progressReaders = new List<IProgressReader>();
            _progressWriters = new List<IProgressWriter>();
        }
        
        public IReadOnlyList<IProgressReader> ProgressReaders => _progressReaders;
        public IReadOnlyList<IProgressWriter> ProgressWriters => _progressWriters;

        public void SaveProgress()
        {
            foreach (IProgressWriter progressWriter in ProgressWriters)
                progressWriter.WriteProgress(_progressService.PlayerProgress);

            string dataToStore = _progressService.PlayerProgress.ToJson();
            PlayerPrefs.SetString(PlayerProgressKey, dataToStore);
            
            ///////
            string path = Path.Combine(Application.persistentDataPath, "Data.json");
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(dataToStore);
                }
            }
            ///////
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(PlayerProgressKey)
                ?.FromJson<PlayerProgress>();

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        public void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (IProgressReader progressReader in gameObject.GetComponentsInChildren<IProgressReader>())
                Register(progressReader);
        }

        private void Register(IProgressReader progressReader)
        {
            if (progressReader is IProgressWriter progressWriter)
                _progressWriters.Add(progressWriter);
            
            _progressReaders.Add(progressReader);
        }
    }
}