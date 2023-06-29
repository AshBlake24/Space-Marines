namespace Roguelike.UI.Windows.Confirmations
{
    public class ResetTutorialWindow : ConfirmationWindow
    {
        protected override void OnConfirm()
        {
            base.OnConfirm();
            ProgressService.ResetTutorial();
        }
    }
}