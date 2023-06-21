using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Localization;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor.Localization
{
    public class TextLocaliserSearchWindow : EditorWindow
    {
        public string Value;
        public Vector2 Scroll;
        private Dictionary<string, string> _dictionary;

        public static void Open()
        {
            TextLocaliserSearchWindow window = CreateInstance<TextLocaliserSearchWindow>();
            window.titleContent = new GUIContent("Localization Search");

            Vector2 mouse = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
            Rect rect = new(mouse.x - 450, mouse.y + 10, 10, 10);
            window.ShowAsDropDown(rect, new Vector2(500, 300));
        }

        private void OnEnable()
        {
            _dictionary = LocalizationSystem.GetDictionaryForEditor();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            Value = EditorGUILayout.TextField(Value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }

        private void GetSearchResults()
        {
            if (Value == null)
                return;

            EditorGUILayout.BeginVertical();
            Scroll = EditorGUILayout.BeginScrollView(Scroll);

            foreach (KeyValuePair<string,string> element in _dictionary)
            {
                if (element.Key.ToLower().Contains(Value.ToLower()) ||
                    element.Value.ToLower().Contains(Value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("Box");
                    Texture cancelIcon = (Texture) Resources.Load(AssetPath.CancelIconPath);
                    GUIContent content = new(cancelIcon);

                    if (GUILayout.Button(content, new GUIStyle(), GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                    {
                        if (EditorUtility.DisplayDialog("Remove Key " + element.Key + "?",
                                "This will remove the element from localization, are you sure?", "Do it"))
                        {
                            LocalizationSystem.Remove(element.Key);
                            AssetDatabase.Refresh();
                            LocalizationSystem.Init();
                            _dictionary = LocalizationSystem.GetDictionaryForEditor();
                        }
                    }

                    EditorGUILayout.TextField(element.Key);
                    EditorGUILayout.LabelField(element.Value);
                    EditorGUILayout.EndHorizontal();
                }
            }
            
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }
    }
}