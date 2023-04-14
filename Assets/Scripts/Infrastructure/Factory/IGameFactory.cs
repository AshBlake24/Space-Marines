using System.Collections.Generic;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        IReadOnlyList<IProgressReader> ProgressReaders { get; }
        IReadOnlyList<IProgressWriter> ProgressWriters { get; }
        GameObject CreatePlayer(Transform playerInitialPoint);

        GameObject GenerateLevel();
        void Cleanup();
    }
}