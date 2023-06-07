using System;
using Roguelike.Audio;
using UnityEngine;

namespace Roguelike.UI.Elements.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private VolumeSlider _musicVolumeSlider;
        [SerializeField] private VolumeSlider _sfxVolumeSlider;
        
        private IAudioService _audioService;

        public void Construct(IAudioService audioService)
        {
            _audioService = audioService;
            _musicVolumeSlider.Init(_audioService);
            _sfxVolumeSlider.Init(_audioService);
        }
    }
}