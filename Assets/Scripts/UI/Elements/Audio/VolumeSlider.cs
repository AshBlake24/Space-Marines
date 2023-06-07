using System;
using Roguelike.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Audio
{
    public class VolumeSlider : MonoBehaviour
    {
        [SerializeField] private AudioChannel _audioChannel;
        [SerializeField] private Slider _slider;
        
        private IAudioService _audioService;

        private void OnEnable() => 
            _slider.onValueChanged.AddListener(OnValueChanged);

        private void OnDisable() => 
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        
        public void Init(IAudioService audioService) => 
            _audioService = audioService;

        private void OnValueChanged(float value) => 
            _audioService.SetChannelVolume(_audioChannel, value);
    }
}