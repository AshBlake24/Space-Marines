namespace Roguelike.UI.Buttons
{
    public class ChangeWeaponButton : UIButton
    {
        protected override void OnButtonClick() => 
            InputService.ChangeWeapon(true);
    }
}