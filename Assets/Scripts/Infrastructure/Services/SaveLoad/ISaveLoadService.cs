using System;
using System.Collections.Generic;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        IEnumerable<IProgressReader> ProgressReaders { get; }
        IEnumerable<IProgressWriter> ProgressWriters { get; }
        PlayerProgress LoadProgressFromPrefs();
        void LoadProgressFromCloud(Action<PlayerProgress> onLoaded);
        void SaveProgress();
        void InformProgressReaders();
        void RegisterProgressWatchers(GameObject gameObject);
        void Cleanup();
    }
}