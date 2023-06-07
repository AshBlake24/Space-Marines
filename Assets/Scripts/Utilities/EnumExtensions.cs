using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Roguelike.StaticData.Levels;

namespace Roguelike.Utilities
{
    public static class EnumExtensions
    {
        public static string ToLabel(this StageId stageId) => 
            ParseCurrentStage(stageId);
        
        public static IEnumerable<T> GetValues<T>() => 
            Enum.GetValues(typeof(T)).Cast<T>();

        private static string ParseCurrentStage(StageId stageId)
        {
            StringBuilder stringBuilder = new();
            
            string stage = stageId.ToString();
            
            foreach (char symb in stage.Where(char.IsDigit))
                stringBuilder.Append(symb);

            stringBuilder.Insert(1, '-');

            return stringBuilder.ToString();
        }
    }
}