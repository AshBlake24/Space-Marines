using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.Audio;
using AudioSettings = Roguelike.Data.AudioSettings;

namespace Roguelike.Audio
{
    public class AudioService : IAudioService
    {
        public const float MaxLinearValue = 1f;
        public const float MinLinearValue = 0.0001f;
        private const float VolumeMultiplicator = 30f;

        private readonly AudioMixer _mixer;
        private readonly AudioSettings _audioSettings;

        public AudioService(IPersistentDataService persistentData)
        {
            _audioSettings = persistentData.PlayerProgress.Settings.AudioSettings;
            _mixer = Resources.Load<AudioMixer>(AssetPath.AudioMixerPath);
            LoadVolumeSettings();
        }

        private void LoadVolumeSettings()
        {
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                _mixer.SetFloat(audioChannel.ToString(), ConvertToVolume(_audioSettings.ChannelsVolume[audioChannel]));
        }

        public void SetChannelVolume(AudioChannel channel, float value)
        {
            _mixer.SetFloat(channel.ToString(), ConvertToVolume(value));
            _audioSettings.ChannelsVolume[channel] = value;
        }

        private static float ConvertToVolume(float value) => 
            Mathf.Log10(value) * VolumeMultiplicator;
    }
}