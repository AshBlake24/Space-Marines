using System;
using Roguelike.Data;
using Roguelike.Localization;
using Roguelike.Utilities;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class LanguageSelector : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown;
        
        private Settings _settings;
        private Language[] _languages;

        public void Construct(Settings settings)
        {
            _settings = settings;
            
            InitLanguages();
            
            _dropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private void OnDestroy() => 
            _dropdown.onValueChanged.RemoveListener(OnLanguageChanged);

        private void InitLanguages()
        {
            _dropdown.ClearOptions();
            _languages = EnumExtensions.GetValues<Language>();

            foreach (Language language in _languages)
                _dropdown.options.Add(new TMP_Dropdown.OptionData(language.ToString()));

            _dropdown.value = _dropdown.options.FindIndex(data => data.text == _settings.CurrentLanguage.ToString());
        }

        private void OnLanguageChanged(int value)
        {
            if (Enum.TryParse(_dropdown.options[value].text, out Language language))
            {
                _settings.ChangeLanguage(language);
            }
            else
            {
                throw new ArgumentNullException(nameof(_dropdown),
                    $"{_dropdown.options[value].text} language does not exist");
            }
        }
    }
}