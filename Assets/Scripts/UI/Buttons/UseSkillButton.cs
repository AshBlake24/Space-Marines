namespace Roguelike.UI.Buttons
{
    public class UseSkillButton : UIButton
    {
        protected override void OnButtonClick() =>
            InputService.UseSkill();
    }
}