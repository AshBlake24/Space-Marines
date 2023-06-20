using System;
using System.Collections.Generic;
using Roguelike.Audio.Service;
using Roguelike.Localization;
using Roguelike.Localization.Service;
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
            CurrentLanguage = Language.English;
            AudioSettings = new List<AudioSettingsData>();
            
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                AudioSettings.Add(new AudioSettingsData(audioChannel));
        }
    }
}