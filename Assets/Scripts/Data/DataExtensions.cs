using UnityEngine;

namespace Roguelike.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj, bool prettyPrint) => 
            JsonUtility.ToJson(obj, prettyPrint);

        public static T FromJson<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}