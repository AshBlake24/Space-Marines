using System;
using System.Collections.Generic;
using Roguelike.Audio.Service;
using Roguelike.Localization;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Data
{
    [Serializable]
    public class Settings
    {
        [SerializeField] private Language _currentLanguage;
        [SerializeField] private List<AudioSettingsData> _audioSettings;

        public Settings()
        {
            _currentLanguage = Language.English;
            _audioSettings = new List<AudioSettingsData>();
            
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                _audioSettings.Add(new AudioSettingsData(audioChannel));
        }
        
        public static event Action LanguageChanged;
        
        public Language CurrentLanguage => _currentLanguage;
        public IReadOnlyList<AudioSettingsData> AudioSettings => _audioSettings;

        public void ChangeLanguage(Language language)
        {
            if (language != _currentLanguage)
            {
                _currentLanguage = language;
                LanguageChanged?.Invoke();
            }
        }
    }
}