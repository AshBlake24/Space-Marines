using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        protected IInputService InputService;

        private void Awake() => 
            InputService = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        protected abstract void OnButtonClick();
    }
}