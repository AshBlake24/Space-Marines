using System.Collections.Generic;
using System.Linq;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;
using UnityEngine.Audio;

namespace Roguelike.Audio
{
    public class AudioService : IAudioService
    {
        public const float MaxLinearValue = 1f;
        public const float MinLinearValue = 0.0001f;
        private const float VolumeMultiplicator = 30f;

        private readonly IPersistentDataService _persistentData;
        private readonly AudioMixer _mixer;

        private Dictionary<AudioChannel, AudioSettingsData> _audioSettings;

        public AudioService(IPersistentDataService persistentData)
        {
            _persistentData = persistentData;
            _mixer = Resources.Load<AudioMixer>(AssetPath.AudioMixerPath);
        }

        public float GetChannelVolumeLinear(AudioChannel channel) => 
            _audioSettings[channel].Value;

        public void SetChannelVolume(AudioChannel channel, float value)
        {
            _mixer.SetFloat(channel.ToString(), ConvertToLogVolume(value));
            _audioSettings[channel].Value = value;
        }

        public void LoadVolumeSettings()
        {
            _audioSettings = _persistentData.PlayerProgress.Settings.AudioSettings
                .ToDictionary(settings => settings.Channel);

            foreach (AudioChannel audioChannel in _audioSettings.Keys)
                _mixer.SetFloat(audioChannel.ToString(), ConvertToLogVolume(_audioSettings[audioChannel].Value));
        }

        private static float ConvertToLogVolume(float value) => 
            Mathf.Log10(value) * VolumeMultiplicator;
    }
}