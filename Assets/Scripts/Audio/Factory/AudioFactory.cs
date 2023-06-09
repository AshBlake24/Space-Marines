using System;
using Roguelike.Audio.Logic;
using Roguelike.Infrastructure.AssetManagement;
using UnityEngine;

namespace Roguelike.Audio.Factory
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetProvider _assetProvider;

        private Transform _audioRoot;

        public AudioFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public Sound CreateAudioSource()
        {
            GameObject gameObject = _assetProvider.Instantiate(AssetPath.SoundPrefabPath, _audioRoot);

            if (gameObject.TryGetComponent(out Sound sound))
                return sound;
            else
                throw new ArgumentNullException(nameof(Sound));
        }

        public void CreateAudioRoot()
        {
            _audioRoot = new GameObject("AudioRoot").transform;
            CreateAudioTickTimer();
        }

        private void CreateAudioTickTimer() => 
            _assetProvider.Instantiate(AssetPath.AudioTickTimerPath, _audioRoot);
    }
}