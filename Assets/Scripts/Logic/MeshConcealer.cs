using UnityEngine;

namespace Roguelike.Logic
{
    public class MeshConcealer : MonoBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshes;

        public MeshRenderer[] Meshes => _meshes;
    }
}