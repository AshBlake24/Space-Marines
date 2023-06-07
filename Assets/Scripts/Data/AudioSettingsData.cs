using System;
using Roguelike.Audio;

namespace Roguelike.Data
{
    [Serializable]
    public class AudioSettingsData
    {
        public AudioChannel Channel;
        public float Value;

        public AudioSettingsData(AudioChannel channel)
        {
            Channel = channel;
            Value = AudioService.MaxLinearValue;
        }
    }
}