using System.Collections.Generic;

namespace Roguelike.Localization
{
    public class LocalizationSystem
    {
        public static Language s_currentLanguage = Language.English;

        public static CSVLoader s_csvLoader;
        private static Dictionary<string, string> s_localizedEnglish;
        private static Dictionary<string, string> s_localizedRussian;
        private static bool s_isInit;

        public static void Init()
        {
            s_csvLoader = new CSVLoader();
            s_csvLoader.LoadCSV();

            UpdateDictionaries();

            s_isInit = true;
        }

        public static void UpdateDictionaries()
        {
            s_localizedEnglish = s_csvLoader.GetDictionaryValues(Language.English.ToString());
            s_localizedRussian = s_csvLoader.GetDictionaryValues(Language.Russian.ToString());
        }

        public static string GetLocalizedValue(string key)
        {
            if (s_isInit == false)
                Init();

            string value = key;

            switch (s_currentLanguage)
            {
                case Language.English:
                    s_localizedEnglish.TryGetValue(key, out value);
                    break;
                case Language.Russian:
                    s_localizedRussian.TryGetValue(key, out value);
                    break;
            }

            return value;
        }

        public static Dictionary<string, string> GetDictionaryForEditor()
        {
            if (s_isInit == false)
                Init();

            return s_localizedEnglish;
        }

        public static void Add(string key, string value)
        {
            if (value.Contains("\""))
                value.Replace('"', '\"');

            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV();
            s_csvLoader.Add(key,value);
            s_csvLoader.LoadCSV();
            
            UpdateDictionaries();
        }
        
        public static void Replace(string key, string value)
        {
            if (value.Contains("\""))
                value.Replace('"', '\"');

            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV();
            s_csvLoader.Edit(key,value);
            s_csvLoader.LoadCSV();
            
            UpdateDictionaries();
        }
        
        public static void Remove(string key)
        {
            s_csvLoader ??= new CSVLoader();
            
            s_csvLoader.LoadCSV();
            s_csvLoader.Remove(key);
            s_csvLoader.LoadCSV();
            
            UpdateDictionaries();
        }
    }
}