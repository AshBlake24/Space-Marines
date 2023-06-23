using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Roguelike.Audio.Logic
{
    [RequireComponent(typeof(AudioSource))]
    public class Sound : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _fadeOutSpeed;

        private ObjectPool<Sound> _pool;
        private float _lifetimeTicks;
        private int _currentTicks;

        public AudioSource AudioSource => _audioSource;

        private void OnDisable() => 
            AudioTickTimer.Tick -= OnTick;

        public void Init(ObjectPool<Sound> pool)
        {
            _pool = pool;
            _currentTicks = 0;
            InitLifetime();
            AudioTickTimer.Tick += OnTick;
        }

        private void InitLifetime()
        {
            float clipLength = AudioSource.clip.length / AudioSource.pitch;
            _lifetimeTicks = Mathf.Ceil(clipLength / AudioTickTimer.TickTime) + 1;
        }

        private void OnTick()
        {
            if (++_currentTicks >= _lifetimeTicks)
                StartCoroutine(FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (AudioSource.volume > 0.01)
            {
                AudioSource.volume -= _fadeOutSpeed;

                yield return null;
            }
            
            ReleaseToPool();
        }
        
        private void ReleaseToPool() =>
            _pool.Release(this);
    }
}