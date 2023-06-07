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
        private const float VolumeMultiplier = 30f;

        private readonly IPersistentDataService _persistentData;
        private readonly AudioMixer _mixer;
        
        public AudioService(IPersistentDataService persistentData)
        {
            _persistentData = persistentData;
            _mixer = Resources.Load<AudioMixer>(AssetPath.AudioMixerPath);
        }

        public void UnmuteChannel(AudioChannel channel) =>
            _mixer.SetFloat(channel.ToString(), 
                ConvertToVolume(_persistentData.PlayerProgress.Settings.AudioSettings.ChannelsVolume[channel]));

        public void MuteChannel(AudioChannel channel)
        {
            SaveChannelVolume(channel);
            _mixer.SetFloat(channel.ToString(), ConvertToVolume(MinLinearValue));
        }

        public void SetChannelVolume(AudioChannel channel, float value)
        {
            _mixer.SetFloat(channel.ToString(), ConvertToVolume(value));
            SaveChannelVolume(channel);
        }

        private void SaveChannelVolume(AudioChannel channel)
        {
            _mixer.GetFloat(channel.ToString(), out float currentValue);
            float convertedValue = Mathf.Pow(-currentValue, 10);
            _persistentData.PlayerProgress.Settings.AudioSettings.ChannelsVolume[channel] = convertedValue;
        }

        private float ConvertToVolume(float linearValue) => 
            Mathf.Log10(linearValue) * VolumeMultiplier;
    }
}