using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Roguelike.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly List<IProgressReader> _progressReaders;
        private readonly List<IProgressWriter> _progressWriters;

        public IReadOnlyList<IProgressReader> ProgressReaders => _progressReaders;
        public IReadOnlyList<IProgressWriter> ProgressWriters => _progressWriters;

        public GameFactory(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _progressReaders = new List<IProgressReader>();
            _progressWriters = new List<IProgressWriter>();
        }
        
        public GameObject CreatePlayer(Transform playerInitialPoint) => 
            InstantiateRegistered(AssetPath.PlayerPath, playerInitialPoint.position);

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabPath, Vector3 postition)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath, postition);
            RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
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