using Roguelike.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace Roguelike.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly ISaveLoadService _saveLoadService;

        public AssetProvider(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public GameObject Instantiate(string path)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            CheckGameObject(path, prefab);
            
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 postition)
        {
            GameObject prefab = Resources.Load<GameObject>(path);
            CheckGameObject(path, prefab);
            
            return Object.Instantiate(prefab, postition, Quaternion.identity);
        }

        public GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = Instantiate(prefabPath);
            _saveLoadService.RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        public GameObject InstantiateRegistered(string prefabPath, Vector3 postition)
        {
            GameObject gameObject = Instantiate(prefabPath, postition);
            _saveLoadService.RegisterProgressWatchers(gameObject);

            return gameObject;
        }

        private static void CheckGameObject(string path, Object prefab)
        {
            if (prefab == null)
                throw new System.ArgumentNullException($"Object at path {path} does not exist");
        }
    }
}