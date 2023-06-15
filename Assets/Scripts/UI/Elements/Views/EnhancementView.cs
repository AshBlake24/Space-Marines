using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements.Views
{
    public class EnhancementView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _tier;

        public void Construct(Sprite icon, int tier)
        {
            _icon.sprite = icon;
            _tier.text = $"Tier {tier}";
        }
    }
}