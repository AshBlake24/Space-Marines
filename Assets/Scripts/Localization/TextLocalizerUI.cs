using TMPro;
using UnityEngine;

namespace Roguelike.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] public LocalizedString _localizedString;

        private void Start()
        {
            _textField.text = _localizedString.Value;
        }
    }
}