using Roguelike.Audio.Factory;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Roguelike.Audio.Logic
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioMixerGroup _outputChannel;
        [SerializeField, Range(0.5f, 1.5f)] private float _pitchMultiplier = 1f;

        private IAudioFactory _audioFactory;
        private ObjectPool<Sound> _audioSourcesPool;
        private AudioClip _audioClip;
        private Sound _sound;
        private float _defaultPitch;
        
        public void Construct(IAudioFactory audioFactory, AudioClip audioClip)
        {
            _audioFactory = audioFactory;
            _audioClip = audioClip;
            InitAudioSourcesPool();
        }

        protected void PlayAudio()
        {
            _sound = _audioSourcesPool.Get();
            _sound.AudioSource.Play();
        }

        private void AdjustPitch(Sound sound) =>
            sound.AudioSource.pitch = Random.value < 0.5f 
                ? _defaultPitch * Random.Range(1 / _pitchMultiplier, 1) 
                : _defaultPitch * Random.Range(1, _pitchMultiplier);

        private bool PitchIsAdjustable() => 
            Mathf.Approximately(_pitchMultiplier, 1f) == false;

        private void InitAudioSourcesPool()
        {
            _audioSourcesPool = new ObjectPool<Sound>(
                CreatePoolItem,
                OnTakeFromPool,
                OnReleaseToPool,
                OnDestroyItem,
                false);
        }

        private Sound CreatePoolItem()
        {
            Sound sound = _audioFactory.CreateAudioSource();
            _defaultPitch = sound.AudioSource.pitch;
            
            return sound;
        }

        private void OnTakeFromPool(Sound sound)
        {
            sound.transform.position = transform.position;
            sound.AudioSource.clip = _audioClip;
            sound.AudioSource.outputAudioMixerGroup = _outputChannel;
            sound.AudioSource.volume = 1f;

            if (PitchIsAdjustable())
                AdjustPitch(sound);

            sound.Init(_audioSourcesPool);
            sound.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(Sound sound) => 
            sound.gameObject.SetActive(false);

        private void OnDestroyItem(Sound sound) => 
            Destroy(sound.gameObject);
    }
}