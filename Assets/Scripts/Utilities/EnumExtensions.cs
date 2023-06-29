using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Roguelike.StaticData.Levels;
using UnityEngine.SceneManagement;

namespace Roguelike.Utilities
{
    public static class EnumExtensions
    {
        private const string Boss = "Boss";

        public static LevelId GetCurrentLevelId()
        {
            Enum.TryParse(SceneManager.GetActiveScene().name, true, out LevelId activeScene);

            return activeScene;
        }
        
        public static string ToLabel(this StageId stageId) => 
            ParseCurrentStage(stageId);
        
        public static T[] GetValues<T>() => 
            (T[])Enum.GetValues(typeof(T));

        private static string ParseCurrentStage(StageId stageId)
        {
            StringBuilder stringBuilder = new();
            
            string stage = stageId.ToString();
            
            foreach (char symb in stage.Where(char.IsDigit))
                stringBuilder.Append(symb);

            if (stringBuilder.Length == 0)
                return TryParseBossStage(stage) ?? string.Empty;

            stringBuilder.Insert(1, '-');

            return stringBuilder.ToString();
        }

        private static string TryParseBossStage(string stage)
        {
            string[] words =  Regex.Split(stage, @"(?<!^)(?=[A-Z])");
            
            return words.FirstOrDefault(word => word == Boss);
        }
    }
}