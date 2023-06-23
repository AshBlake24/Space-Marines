using Roguelike.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roguelike.UI.Tabs
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private TabGroup _tabGroup;
        [SerializeField] private LocalizedString _localizedString;
        
        public TextMeshProUGUI Text { get; private set; }

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
            _tabGroup.Subscribe(this);
        }

        public void OnPointerEnter(PointerEventData eventData) => 
            _tabGroup.OnTabEnter(this);

        public void OnPointerExit(PointerEventData eventData) => 
            _tabGroup.OnTabExit(this);

        public void OnPointerClick(PointerEventData eventData) => 
            _tabGroup.OnTabSelected(this);

        public void OnSelected() => Text.text = $"<u>{_localizedString.Value}</u>";

        public void OnDeselected() => Text.text = _localizedString.Value;
    }
}