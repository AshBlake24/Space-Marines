﻿using System.Collections.Generic;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Localization;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor
{
    public class TextLocalizerEditWindow : EditorWindow
    {
        public string key;
        public string value;
        
        public static void Open(string key)
        {
            TextLocalizerEditWindow window = CreateInstance<TextLocalizerEditWindow>();
            window.titleContent = new GUIContent("Localizer Window");
            window.ShowUtility();
            window.key = key;
        }

        public void OnGUI()
        {
            key = EditorGUILayout.TextField("Key: ", key);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value: ", GUILayout.MaxWidth(50));
            EditorStyles.textArea.wordWrap = true;
            value = EditorGUILayout.TextArea(value, EditorStyles.textArea, GUILayout.Height(100), GUILayout.Width(400));
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                if (LocalizationSystem.GetLocalizedValue(key) != string.Empty)
                {
                    LocalizationSystem.Replace(key, value);
                }
                else
                {
                    LocalizationSystem.Add(key, value);
                }
            }

            minSize = new Vector2(460, 250);
            maxSize = minSize;
        }
    }

    public class TextLocaliserSearchWindow : EditorWindow
    {
        public string value;
        public Vector2 scroll;
        public Dictionary<string, string> dictionary;

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
            dictionary = LocalizationSystem.GetDictionaryForEditor();
        }

        public void OnGUI()
        {
            EditorGUILayout.BeginHorizontal("Box");
            EditorGUILayout.LabelField("Search: ", EditorStyles.boldLabel);
            value = EditorGUILayout.TextField(value);
            EditorGUILayout.EndHorizontal();

            GetSearchResults();
        }

        private void GetSearchResults()
        {
            if (value == null)
                return;

            EditorGUILayout.BeginVertical();
            scroll = EditorGUILayout.BeginScrollView(scroll);

            foreach (KeyValuePair<string,string> element in dictionary)
            {
                if (element.Key.ToLower().Contains(value.ToLower()) ||
                    element.Value.ToLower().Contains(value.ToLower()))
                {
                    EditorGUILayout.BeginHorizontal("Box");
                    Texture cancelIcon = (Texture) Resources.Load(AssetPath.CancelIconPath);
                    GUIContent content = new GUIContent(cancelIcon);

                    if (GUILayout.Button(content, new GUIStyle(), GUILayout.MaxWidth(20), GUILayout.MaxHeight(20)))
                    {
                        if (EditorUtility.DisplayDialog("Remove Key " + element.Key + "?",
                                "This will remove the element from localization, are you sure?", "Do it"))
                        {
                            LocalizationSystem.Remove(element.Key);
                            AssetDatabase.Refresh();
                            LocalizationSystem.Init();
                            dictionary = LocalizationSystem.GetDictionaryForEditor();
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