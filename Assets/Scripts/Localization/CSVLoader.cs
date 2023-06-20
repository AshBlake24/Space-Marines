using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Roguelike.Localization
{
    public class CSVLoader
    {
        private readonly char _lineSeparator = '\n';
        private readonly char _textSurroundings = '"';
        private readonly string[] _fieldSeparator = {"\",\""};

        private TextAsset _csvFile;

        #region Editor
#if UNITY_EDITOR
        public void Add(string csvPath, string key, string value)
        {
            string appended = string.Format("\n\"{0}\",\"{1}\",\"\",\"\"", key, value);
            File.AppendAllText(csvPath, appended);
            
            UnityEditor.AssetDatabase.Refresh();
        }

        public void Remove(string csvPath, string key)
        {
            string[] lines = _csvFile.text.Split(_lineSeparator);
            string[] keys = new string[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                keys[i] = line.Split(_fieldSeparator, StringSplitOptions.None)[0];
            }

            int index = -1;

            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].Contains(key))
                {
                    index = i;
                    break;
                }
            }

            if (index > -1)
            {
                string[] newLines = lines.Where(s => s != lines[index]).ToArray();
                string replaced = string.Join(_lineSeparator.ToString(), newLines);
                
                File.WriteAllText(csvPath, replaced);
            }
        }

        public void Edit(string csvPath, string key, string value)
        {
            Remove(csvPath, key);
            Add(csvPath, key, value);
        }
#endif
        #endregion
        
        public void LoadCSV(string csvPath) => 
            _csvFile = Resources.Load<TextAsset>(csvPath);

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            string[] lines = _csvFile.text.Split(_lineSeparator);
            string[] headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);
            int attributeIndex = GetAttributeIndex(attributeId, headers);

            if (attributeIndex == -1)
                throw new ArgumentOutOfRangeException(nameof(attributeId), $"Attribute \"{attributeId}\" not found");

            Dictionary<string, string> dictionary = ParseTextToDictionary(lines, attributeIndex);

            return dictionary;
        }

        private static int GetAttributeIndex(string attributeId, string[] headers)
        {
            int attributeIndex = -1;
            
            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].Contains(attributeId))
                {
                    attributeIndex = i;
                    break;
                }
            }

            return attributeIndex;
        }

        private static void TryAddValue(Dictionary<string, string> dictionary, string[] fields, int attributeIndex)
        {
            string key = fields[0];

            if (dictionary.ContainsKey(key))
                return;

            string value = fields[attributeIndex];

            dictionary.Add(key, value);
        }

        private Dictionary<string, string> ParseTextToDictionary(string[] lines, int attributeIndex)
        {
            Dictionary<string, string> dictionary = new();
            Regex csvParser = new(",(?(:[^\"]*\"[^\"]*\")(?![^\"]*\"))");

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = csvParser.Split(line);

                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = fields[j].TrimStart(' ', _textSurroundings);
                    fields[j] = fields[j].TrimEnd(_textSurroundings);
                }

                if (fields.Length > attributeIndex) 
                    TryAddValue(dictionary, fields, attributeIndex);
            }

            return dictionary;
        }
    }
}