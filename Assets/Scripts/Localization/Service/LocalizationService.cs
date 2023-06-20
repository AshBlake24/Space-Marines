using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;

namespace Roguelike.Localization.Service
{
    public class LocalizationService : ILocalizationService
    {
        private const string LocalizationFile = "Assets/Resources/Localization/Localization.csv";
        
        private static IPersistentDataService s_persistentData;
        private static Dictionary<string, string> s_localisedEnglish;
        private static Dictionary<string, string> s_localisedRussian;
        private static Dictionary<string, string> s_localisedTurkish;
        private static CSVLoader s_csvLoader;

        public LocalizationService(IPersistentDataService persistentData)
        {
            s_persistentData = persistentData;
        }

        public void Init()
        {
            s_csvLoader = new CSVLoader();
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);

            UpdateDictionaries();
        }

        private static void UpdateDictionaries()
        {
            s_localisedEnglish = s_csvLoader.GetDictionaryValues(Language.English.ToString());
            s_localisedRussian = s_csvLoader.GetDictionaryValues(Language.Russian.ToString());
            s_localisedTurkish = s_csvLoader.GetDictionaryValues(Language.Turkish.ToString());
        }

        public static string GetLocalisedValue(string key)
        {
            string value = key;

            switch (s_persistentData.PlayerProgress.Settings.CurrentLanguage)
            {
                case Language.English:
                    s_localisedEnglish.TryGetValue(key, out value);
                    break;
                case Language.Russian:
                    s_localisedRussian.TryGetValue(key, out value);
                    break;
                case Language.Turkish:
                    s_localisedTurkish.TryGetValue(key, out value);
                    break;
            }

            return value;
        }

        public static void Add(string key, string value)
        {
            if (value.Contains('\"'))
                value.Replace('"', '\"');

            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            s_csvLoader.Add(LocalizationFile, key, value);
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            
            UpdateDictionaries();
        }
        
        public static void Replace(string key, string value)
        {
            if (value.Contains('\"'))
                value.Replace('"', '\"');

            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            s_csvLoader.Edit(LocalizationFile, key, value);
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            
            UpdateDictionaries();
        }
        
        public static void Remove(string key)
        {
            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            s_csvLoader.Remove(LocalizationFile, key);
            s_csvLoader.LoadCSV(AssetPath.LocalizationPath);
            
            UpdateDictionaries();
        }
    }
}