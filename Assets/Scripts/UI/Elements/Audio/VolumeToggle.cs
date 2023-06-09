using Roguelike.Audio.Service;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Audio
{
    public class VolumeToggle : MonoBehaviour
    {
        [SerializeField] private AudioChannel _audioChannel;
        [SerializeField] private Toggle _toggle;
        
        private IAudioService _audioService;

        private void OnEnable() => 
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);

        private void OnDisable() => 
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);

        public void Init(IAudioService audioService)
        {
            _audioService = audioService;
            _toggle.isOn = _audioService.GetChannelMute(_audioChannel);
            OnToggleValueChanged(_toggle.isOn);
        }

        private void OnToggleValueChanged(bool isOn) => 
            _audioService.SetChannelMute(_audioChannel, !isOn);
    }
}