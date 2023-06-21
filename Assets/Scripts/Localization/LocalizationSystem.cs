using System;
using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Localization
{
    public static class LocalizationSystem
    {
        public const string LocalizationFilePath = "Assets/Resources/Localization/Localization.csv";

        private static Language s_currentLanguage;
        private static IPersistentDataService s_persistentDataService;
        private static Dictionary<string, string> s_localizedEnglish;
        private static Dictionary<string, string> s_localizedRussian;
        private static Dictionary<string, string> s_localizedTurkish;
        private static CSVLoader s_csvLoader;
        private static bool s_isInit;

        public static void Construct(IPersistentDataService persistentDataService)
        {
            s_persistentDataService = persistentDataService;
        }

        public static void Init()
        {
            s_csvLoader = new CSVLoader();
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);

            UpdateDictionaries();

            s_isInit = true;
        }

        public static string GetLocalizedValue(string key)
        {
            if (s_isInit == false)
                Init();

            string value = key;

            if (Application.isPlaying)
            {
                s_currentLanguage = s_persistentDataService.PlayerProgress != null 
                    ? s_persistentDataService.PlayerProgress.Settings.CurrentLanguage 
                    : Language.English;
            }
            else
            {
                s_currentLanguage = Language.English;
            }

            switch (s_currentLanguage)
            {
                case Language.English:
                    s_localizedEnglish.TryGetValue(key, out value);
                    break;
                case Language.Russian:
                    s_localizedRussian.TryGetValue(key, out value);
                    break;
                case Language.Turkish:
                    s_localizedTurkish.TryGetValue(key, out value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Language));
            }

            return value;
        }

        public static Dictionary<string, string> GetDictionaryForEditor()
        {
            if (s_isInit == false)
                Init();

            return s_localizedEnglish;
        }
        
        private static void UpdateDictionaries()
        {
            s_localizedEnglish = s_csvLoader.GetDictionaryValues(Language.English.ToString());
            s_localizedRussian = s_csvLoader.GetDictionaryValues(Language.Russian.ToString());
            s_localizedTurkish = s_csvLoader.GetDictionaryValues(Language.Turkish.ToString());
        }
        
#if UNITY_EDITOR
        public static void Add(string key, string value)
        {
            TrimValue(value);
            UpdateCSV(() => s_csvLoader.Add(LocalizationFilePath, key, value));
        }

        public static void Replace(string key, string value)
        {
            TrimValue(value);
            UpdateCSV(() => s_csvLoader.Edit(LocalizationFilePath, key, value));
        }

        public static void Remove(string key)
        {
            UpdateCSV(() => s_csvLoader.Remove(LocalizationFilePath, key));
        }

        private static void TrimValue(string value)
        {
            if (value.Contains("\""))
                value.Replace('"', '\"');
        }

        private static void UpdateCSV(Action action)
        {
            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            action.Invoke();
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);

            UpdateDictionaries();
        }

#endif
    }
}