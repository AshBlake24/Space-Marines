using TMPro;
using UnityEngine;

namespace Roguelike.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocaliserUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private LocalizedString _localizedString;

        private void Start()
        {
            _text.text = _localizedString.Value;
        }
    }
}