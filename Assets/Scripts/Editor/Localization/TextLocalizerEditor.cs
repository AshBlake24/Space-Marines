using Roguelike.Localization;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor.Localization
{
    public class TextLocalizerEditWindow : EditorWindow
    {
        public string Key;
        public string Value;
        
        public static void Open(string key)
        {
            TextLocalizerEditWindow window = CreateInstance<TextLocalizerEditWindow>();
            window.titleContent = new GUIContent("Localizer Window");
            window.ShowUtility();
            window.Key = key;
        }

        public void OnGUI()
        {
            Key = EditorGUILayout.TextField("Key: ", Key);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value: ", GUILayout.MaxWidth(50));
            EditorStyles.textArea.wordWrap = true;
            Value = EditorGUILayout.TextArea(Value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                if (LocalizationSystem.GetLocalizedValue(Key) != string.Empty)
                {
                    LocalizationSystem.Replace(Key, Value);
                }
                else
                {
                    LocalizationSystem.Add(Key, Value);
                }
            }

            minSize = new Vector2(460, 250);
            maxSize = minSize;
        }
    }
}