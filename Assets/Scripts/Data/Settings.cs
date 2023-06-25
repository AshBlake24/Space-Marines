using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Roguelike.Audio.Service;
using Roguelike.Localization;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Data
{
    [Serializable]
    public class Settings
    {
        private const string EnglishLanguageCode = "en";
        private const string RussianLanguageCode = "ru";
        private const string TurkishLanguageCode = "tr";
        
        [SerializeField] private Language _currentLanguage;
        [SerializeField] private List<AudioSettingsData> _audioSettings;

        public Settings()
        {
            InitLanguage();
            _audioSettings = new List<AudioSettingsData>();
            
            foreach (AudioChannel audioChannel in EnumExtensions.GetValues<AudioChannel>())
                _audioSettings.Add(new AudioSettingsData(audioChannel));
        }

        private void InitLanguage()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            _currentLanguage = languageCode switch
            {
                EnglishLanguageCode => Language.English,
                RussianLanguageCode => Language.Russian,
                TurkishLanguageCode => Language.Turkish,
                _ => throw new ArgumentOutOfRangeException(nameof(languageCode))
            };
#else
            _currentLanguage = Language.English;
#endif
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