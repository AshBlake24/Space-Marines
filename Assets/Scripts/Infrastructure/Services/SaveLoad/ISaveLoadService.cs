using System.Collections.Generic;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        IReadOnlyList<IProgressReader> ProgressReaders { get; }
        IReadOnlyList<IProgressWriter> ProgressWriters { get; }
        PlayerProgress LoadProgress();
        void SaveProgress();
        void RegisterProgressWatchers(GameObject gameObject);
        void Cleanup();
    }
}