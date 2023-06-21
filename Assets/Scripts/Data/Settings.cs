using System;
using System.Collections.Generic;
using Roguelike.Audio.Service;
using Roguelike.Localization;
using Roguelike.Utilities;

namespace Roguelike.Data
{
    [Serializable]
    public class Settings
    {
        public Language CurrentLanguage;
        public List<AudioSettingsData> AudioSettings;

        public Settings()
        {
            CurrentLanguage = Language.Russian;
            AudioSettings = new List<AudioSettingsData>();
            
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                AudioSettings.Add(new AudioSettingsData(audioChannel));
        }
    }
}