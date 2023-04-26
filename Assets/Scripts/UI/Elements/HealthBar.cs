using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _currentHealthImage;
        [SerializeField] private TextMeshProUGUI _currentHealthText;

        public void SetValue(int current, int max)
        {
            _currentHealthImage.fillAmount = (float) current / max;
            _currentHealthText.text = $"{current}/{max}";
        }
    }
}