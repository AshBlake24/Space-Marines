using Roguelike.Audio.Service;
using UnityEngine;

namespace Roguelike.UI.Elements.Audio
{
    public class AudioSettingsWindow : MonoBehaviour
    {
        [Header("Sliders")]
        [SerializeField] private VolumeSlider _musicVolumeSlider;
        [SerializeField] private VolumeSlider _sfxVolumeSlider;
        
        [Header("Toggles")]
        [SerializeField] private VolumeToggle _musicVolumeToggle;
        [SerializeField] private VolumeToggle _sfxVolumeToggle;
        
        public void Construct(IAudioService audioService)
        {
            _musicVolumeSlider.Init(audioService);
            _sfxVolumeSlider.Init(audioService);
            _musicVolumeToggle.Init(audioService);
            _sfxVolumeToggle.Init(audioService);
        }
    }
}