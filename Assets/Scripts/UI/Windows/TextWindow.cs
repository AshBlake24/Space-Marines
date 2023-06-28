using TMPro;
using UnityEngine;

namespace Roguelike.UI.Windows
{
    public class TextWindow : BaseWindow
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public void InitText(string text)
        {
            _text.text = text;
        }
    }
}