using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;

namespace Roguelike.Localization
{
    public class LocalizationService
    {
        private const string EnglishLocalizationCode = "en";
        private const string RussianLocalizationCode = "ru";
        private const string TurkishLocalizationCode = "tr";
        
        public static Language CurrentLanguage = Language.English;

        private static Dictionary<string, string> s_localisedEn;
        private static Dictionary<string, string> s_localisedRu;
        private static Dictionary<string, string> s_localisedTr;

        private static bool s_isInit;

        public static void Init()
        {
            CSVLoader csvLoader = new();
            csvLoader.LoadCSV(AssetPath.LocalizationPath);

            s_localisedEn = csvLoader.GetDictionaryValues(EnglishLocalizationCode);
            s_localisedRu = csvLoader.GetDictionaryValues(RussianLocalizationCode);
            s_localisedTr = csvLoader.GetDictionaryValues(TurkishLocalizationCode);

            s_isInit = true;
        }

        public static string GetLocalisedValue(string key)
        {
            string value = key;

            switch (CurrentLanguage)
            {
                case Language.English:
                    s_localisedEn.TryGetValue(key, out value);
                    break;
                case Language.Russian:
                    s_localisedRu.TryGetValue(key, out value);
                    break;
                case Language.Turkish:
                    s_localisedTr.TryGetValue(key, out value);
                    break;
            }

            return value;
        }
    }
}