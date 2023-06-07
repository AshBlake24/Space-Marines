using System;
using System.Collections.Generic;
using Roguelike.Audio;
using Roguelike.Utilities;

namespace Roguelike.Data
{
    [Serializable]
    public class Settings
    {
        public List<AudioSettingsData> AudioSettings;

        public Settings()
        {
            AudioSettings = new List<AudioSettingsData>();
            
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                AudioSettings.Add(new AudioSettingsData(audioChannel));
        }
    }
}