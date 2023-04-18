using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Logic
{
    public class VFX : MonoBehaviour
    {
        private ObjectPool<VFX> _pool;

        public void Counstruct(ObjectPool<VFX> pool)
        {
            _pool = pool;
        }

        private void OnParticleSystemStopped()
        {
            _pool.AddInstance(this);
        }
    }
}