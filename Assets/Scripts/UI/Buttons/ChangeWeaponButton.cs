using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;

namespace Roguelike.UI.Buttons
{
    public class ChangeWeaponButton : UIButton
    {
        private IInputService _inputService;

        private void Awake() => 
            _inputService = AllServices.Container.Single<IInputService>();
        
        protected override void OnButtonClick() => 
            _inputService.ChangeWeapon(true);
    }
}