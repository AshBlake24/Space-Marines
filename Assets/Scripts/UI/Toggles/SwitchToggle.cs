using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Toggles
{
    public class SwitchToggle : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private Image _image;
        [SerializeField] protected Sprite _enabledSprite;
        [SerializeField] protected Sprite _disabledSprite;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnSwitch);
            
            if (_toggle.isOn)
                OnSwitch(true);
        }

        private void OnSwitch(bool isOn)
        {
            if (isOn)
                Enable();
            else
                Disable();
        }

        private void Enable() => 
            _image.sprite = _enabledSprite;

        private void Disable() => 
            _image.sprite = _disabledSprite;
    }
}