using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Roguelike.Localization
{
    public class CSVLoader
    {
        private readonly char _lineSeparator = '\n';
        private readonly char _surround = '"';
        private readonly string[] _fieldSeparator = { "\",\""};
        
        private TextAsset _csvFile;

        public void LoadCSV(string csvPath) => 
            _csvFile = Resources.Load<TextAsset>(csvPath);

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            Dictionary<string, string> dictionary = new();

            string[] lines = _csvFile.text.Split(_lineSeparator);
            string[] headers = lines[0].Split(_fieldSeparator, StringSplitOptions.None);
            int attributeIndex = GetAttributeIndex(attributeId, headers);

            ParseTextToDictionary(dictionary, lines, attributeIndex);

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

        private void ParseTextToDictionary(Dictionary<string, string> dictionary, string[] lines, int attributeIndex)
        {
            Regex csvParser = new(",(?(:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = csvParser.Split(line);

                for (int j = 0; j < fields.Length; j++)
                {
                    fields[j] = fields[j].TrimStart(' ', _surround);
                    fields[j] = fields[j].TrimEnd(_surround);
                }

                if (fields.Length > attributeIndex) 
                    TryAddValue(dictionary, fields, attributeIndex);
            }
        }

        private static void TryAddValue(Dictionary<string, string> dictionary, string[] fields, int attributeIndex)
        {
            string key = fields[0];

            if (dictionary.ContainsKey(key))
                return;

            string value = fields[attributeIndex];

            dictionary.Add(key, value);
        }
    }
}