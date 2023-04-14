using UnityEngine;

namespace Roguelike.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
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

        private static void CheckGameObject(string path, Object prefab)
        {
            if (prefab == null)
                throw new System.ArgumentNullException($"Object at path {path} does not exist");
        }
    }
}