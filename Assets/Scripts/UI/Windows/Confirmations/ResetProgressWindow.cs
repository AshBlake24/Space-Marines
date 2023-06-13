namespace Roguelike.UI.Windows.Confirmations
{
    public class ResetProgressWindow : ConfirmationWindow
    {
        protected override void OnConfirm()
        {
            base.OnConfirm();
            ProgressService.ResetAllProgress();
        }
    }
}