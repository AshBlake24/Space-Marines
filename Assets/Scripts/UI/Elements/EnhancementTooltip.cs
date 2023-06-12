using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class EnhancementTooltip : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _header;
        [SerializeField] private TextMeshProUGUI _content;
        [SerializeField] private TextMeshProUGUI _tier;
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private int _characterWrapLimit;
        
        public void SetText(string content, string header, string currentTier)
        {
            SetHeader(header, currentTier);
            SetContent(content);
            InitLayoutElement();
        }

        private void SetContent(string content) => 
            _content.text = content;

        private void SetHeader(string header, string currentTier)
        {
            _header.text = header;
            _tier.text = $"Tier {currentTier}";
        }

        private void InitLayoutElement()
        {
            int headerLength = _header.text.Length;
            int contentLength = _content.text.Length;

            _layoutElement.enabled = (headerLength > _characterWrapLimit || contentLength > _characterWrapLimit);
        }
    }
}