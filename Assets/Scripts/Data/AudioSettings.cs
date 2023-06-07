using System;
using System.Collections.Generic;
using Roguelike.Audio;

namespace Roguelike.Data
{
    [Serializable]
    public class AudioSettings
    {
        public readonly Dictionary<AudioChannel, float> ChannelsVolume;
        
        public AudioSettings()
        {
            ChannelsVolume = new Dictionary<AudioChannel, float>
            {
                {AudioChannel.Master, AudioService.MaxLinearValue},
                {AudioChannel.Music, AudioService.MaxLinearValue},
                {AudioChannel.SFX, AudioService.MaxLinearValue}
            };
        }
    }
}