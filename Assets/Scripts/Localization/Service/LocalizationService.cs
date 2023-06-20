using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services.PersistentData;

namespace Roguelike.Localization.Service
{
    public class LocalizationService : ILocalizationService
    {
        private static IPersistentDataService s_persistentData;
        private static Dictionary<string, string> s_localisedEnglish;
        private static Dictionary<string, string> s_localisedRussian;
        private static Dictionary<string, string> s_localisedTurkish;

        public LocalizationService(IPersistentDataService persistentData)
        {
            s_persistentData = persistentData;
        }

        public void Init()
        {
            CSVLoader csvLoader = new();
            csvLoader.LoadCSV(AssetPath.LocalizationPath);

            s_localisedEnglish = csvLoader.GetDictionaryValues(Language.English.ToString());
            s_localisedRussian = csvLoader.GetDictionaryValues(Language.Russian.ToString());
            s_localisedTurkish = csvLoader.GetDictionaryValues(Language.Turkish.ToString());
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
    }
}