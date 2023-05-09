using Roguelike.Infrastructure.Services;
using UnityEngine;

namespace Roguelike.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 postition);
        GameObject Instantiate(string path, Transform parent);
        GameObject InstantiateRegistered(string prefabPath);
        GameObject InstantiateRegistered(string prefabPath, Vector3 postition);
    }
}