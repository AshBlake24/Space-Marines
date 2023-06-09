using System;
using Roguelike.Audio.Factory;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Roguelike.Audio.Logic
{
    public abstract class AudioPlayer : MonoBehaviour
    {
        [Header("Audio Settings")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioMixerGroup _outputChannel;
        [SerializeField, Range(0.5f, 1.5f)] private float _pitchMultiplier = 1f;

        private IAudioFactory _audioFactory;
        public void Construct(IAudioFactory audioFactory)
        {
            _audioFactory = audioFactory;
        }

        protected void PlayAudio()
        {
            AudioSource audioSource = _audioFactory.CreateAudioSource();
            
            audioSource.transform.position = transform.position;
            audioSource.clip = _audioClip;
            audioSource.outputAudioMixerGroup = _outputChannel;

            if (PitchIsAdjustable())
                AdjustPitch(audioSource);
            
            audioSource.Play();
            
            float lifetime = audioSource.clip.length / audioSource.pitch;
            Destroy(audioSource, lifetime);
        }

        private void AdjustPitch(AudioSource audioSource)
        {
            float defaultPitch = audioSource.pitch;
            
            audioSource.pitch = Random.value < 0.5f 
                ? defaultPitch * Random.Range(1 / _pitchMultiplier, 1) 
                : audioSource.pitch = defaultPitch * Random.Range(1, _pitchMultiplier);
            
            Debug.Log($"Default pitch: {defaultPitch}");
            Debug.Log($"New pitch: {audioSource.pitch}");
        }

        private bool PitchIsAdjustable() => 
            Mathf.Approximately(_pitchMultiplier, 1f) == false;
    }
}