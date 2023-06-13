using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] protected Button Button;
        
        private void OnEnable() => 
            Button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            Button.onClick.RemoveListener(OnButtonClick);

        protected abstract void OnButtonClick();
    }
}