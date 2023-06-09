using System;
using Roguelike.Infrastructure.AssetManagement;
using UnityEngine;

namespace Roguelike.Audio.Factory
{
    public class AudioFactory : IAudioFactory
    {
        private readonly IAssetProvider _assetProvider;

        public AudioFactory(IAssetProvider assetProvider) => 
            _assetProvider = assetProvider;

        public AudioSource CreateAudioSource()
        {
            GameObject gameObject = _assetProvider.Instantiate(AssetPath.AudioSourcePath);

            if (gameObject.TryGetComponent(out AudioSource audioSource))
                return audioSource;
            else
                throw new ArgumentNullException(nameof(AudioSource));
        }
    }
}