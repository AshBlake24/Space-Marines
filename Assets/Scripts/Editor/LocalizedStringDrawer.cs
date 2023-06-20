using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Localization;
using UnityEditor;
using UnityEngine;

namespace Roguelike.Editor
{
    [CustomPropertyDrawer(typeof(LocalizedString))]
    public class LocalizedStringDrawer : PropertyDrawer
    {
        private bool _dropdown;
        private float _height;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (_dropdown)
                return _height + 25;

            return 20;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            position.width -= 34;
            position.height = 18;
            
            Rect valueRect = new(position);
            valueRect.x += 15;
            valueRect.width -= 15;

            Rect foldButtonRect = new(position);
            foldButtonRect.width = 15;

            _dropdown = EditorGUI.Foldout(foldButtonRect, _dropdown, "");

            position.x += 15;
            position.width -= 15;

            SerializedProperty key = property.FindPropertyRelative("key");
            key.stringValue = EditorGUI.TextField(position, key.stringValue);

            position.x += position.width + 2;
            position.width = 17;
            position.height = 17;

            Texture searchIcon = (Texture) Resources.Load(AssetPath.SearchIconPath);
            GUIContent searchContent = new(searchIcon);

            if (GUI.Button(position, searchContent, new GUIStyle()))
            {
                TextLocaliserSearchWindow.Open();
            }

            position.x += position.width + 2;
            
            Texture storeIcon = (Texture) Resources.Load(AssetPath.AddIconPath);
            GUIContent storeContent = new(storeIcon);

            if (GUI.Button(position, storeContent, new GUIStyle()))
            {
                TextLocalizerEditWindow.Open(key.stringValue);
            }

            if (_dropdown)
            {
                string value = LocalizationSystem.GetLocalizedValue(key.stringValue);
                GUIStyle style = GUI.skin.box;
                _height = style.CalcHeight(new GUIContent(value), valueRect.width);
            
                valueRect.height = _height;
                valueRect.y += 21;
                EditorGUI.LabelField(valueRect, value, EditorStyles.wordWrappedLabel);
            }
            
            EditorGUI.EndProperty();
        }
    }
}