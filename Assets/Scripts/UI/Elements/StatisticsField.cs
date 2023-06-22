using Roguelike.Localization;
using TMPro;
using UnityEngine;

namespace Roguelike.UI.Elements
{
    public class StatisticsField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textField;
        [SerializeField] private LocalizedString _localizedString;

        public void SetStatsValue(int value) => 
            _textField.text = string.Format(_localizedString.Value, value);
    }
}