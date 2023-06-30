using System.IO;
using Roguelike.Data;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Localization;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear Prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        
        [MenuItem("Tools/Save Progress To JSON")]
        public static void SaveProgress()
        {
            if (EditorApplication.isPlaying == false)
                return;
            
            AllServices.Container
                .Single<ISaveLoadService>()
                .SaveProgress();

            string dataToStore = AllServices.Container
                .Single<IPersistentDataService>()
                .PlayerProgress
                .ToJson(prettyPrint: true);
            
            SaveToFile(dataToStore);
            
            Debug.Log("Progress Saved");
        }

        [MenuItem("Tools/Sort Localization CSV")]
        public static void SortLocalizationCSV()
        {
            CSVLoader csvLoader = new();
            csvLoader.LoadCSV(AssetPath.LocalizationPath);
            csvLoader.SortByKeyNames(LocalizationSystem.LocalizationFilePath);
            AssetDatabase.Refresh();
        }
        
        [MenuItem("Tools/Reload Configs")]
        public static void ReloadConfigs()
        {
            IStaticDataService staticDataService = AllServices.Container.Single<IStaticDataService>();
            staticDataService.Load();
        }
        
        private static void SaveToFile(string dataToStore)
        {
            string path = Path.Combine(Application.persistentDataPath, "Data.json");

            using FileStream fileStream = new(path, FileMode.Create);
            using StreamWriter streamWriter = new(fileStream);

            streamWriter.Write(dataToStore);
        }
    }
}