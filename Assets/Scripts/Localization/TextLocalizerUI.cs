using Roguelike.Data;
using TMPro;
using UnityEngine;

namespace Roguelike.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizerUI : MonoBehaviour
    {
        [SerializeField] private LocalizedString _localizedString;
        
        private TextMeshProUGUI _textField;

        private void Awake() => 
            _textField = GetComponent<TextMeshProUGUI>();

        private void OnEnable() => 
            Settings.LanguageChanged += OnLanguageChanged;

        private void OnDisable() => 
            Settings.LanguageChanged -= OnLanguageChanged;

        private void Start() => OnLanguageChanged();

        private void OnLanguageChanged() => 
            _textField.text = _localizedString.Value;
    }
}