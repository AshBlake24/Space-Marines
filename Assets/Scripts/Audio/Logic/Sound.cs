using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Audio.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private ObjectPool<Sound> _pool;
        private float _lifetimeTicks;
        private int _currentTicks;

        public AudioSource AudioSource => _audioSource;

        public void Init(ObjectPool<Sound> pool, float lifetime)
        {
            _pool = pool;
            _currentTicks = 0;
            _lifetimeTicks = Mathf.Ceil(lifetime / AudioTickTimer.TickTime);
            AudioTickTimer.Tick += OnTick;
        }

        private void OnDisable() => 
            AudioTickTimer.Tick -= OnTick;

        private void OnTick()
        {
            if (++_currentTicks >= _lifetimeTicks)
                ReleaseToPool();
        }

        private void ReleaseToPool() =>
            _pool.Release(this);
    }
}