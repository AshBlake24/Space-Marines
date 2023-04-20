using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.UI
{
    [RequireComponent(typeof(Button))]
    public class ChangeWeaponButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        
        private IInputService _inputService;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClick);

        private void OnButtonClick() => 
            _inputService.ChangeWeapon(true);
    }
}