using System;
using Roguelike.Audio.Service;

namespace Roguelike.Data
{
    [Serializable]
    public class AudioSettingsData
    {
        public AudioChannel Channel;
        public float Value;
        public bool IsMuted;

        public AudioSettingsData(AudioChannel channel)
        {
            Channel = channel;
            Value = AudioService.MaxLinearValue;
            IsMuted = false;
        }
    }
}