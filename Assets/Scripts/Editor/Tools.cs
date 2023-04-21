using System.IO;
using Roguelike.Data;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
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
                .ToJson();
            
            SaveToFile(dataToStore);
            
            Debug.Log("Progress Saved");
        }
        
        private static void SaveToFile(string dataToStore)
        {
            string path = Path.Combine(Application.persistentDataPath, "Data.json");

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.Write(dataToStore);
                }
            }
        }
    }
}