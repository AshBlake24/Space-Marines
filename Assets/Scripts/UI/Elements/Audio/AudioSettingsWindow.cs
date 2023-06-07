using Roguelike.Audio;
using UnityEngine;

namespace Roguelike.UI.Elements.Audio
{
    public class AudioSettingsWindow : MonoBehaviour
    {
        [SerializeField] private VolumeSlider _musicVolumeSlider;
        [SerializeField] private VolumeSlider _sfxVolumeSlider;
        
        public void Construct(IAudioService audioService)
        {
            _musicVolumeSlider.Init(audioService);
            _sfxVolumeSlider.Init(audioService);
        }
    }
}