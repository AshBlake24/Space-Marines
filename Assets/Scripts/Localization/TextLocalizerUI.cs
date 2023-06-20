using TMPro;
using UnityEngine;

namespace Roguelike.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocalizerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        public LocalizedString localizedString;

        private void Start()
        {
            _textField.text = localizedString.Value;
        }
    }
}