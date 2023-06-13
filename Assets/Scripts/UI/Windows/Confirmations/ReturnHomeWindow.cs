namespace Roguelike.UI.Windows.Confirmations
{
    public class ReturnHomeWindow : ConfirmationWindow
    {
        protected override void OnConfirm() =>
            SceneLoadingService.Load(StaticData.GameConfig.StartScene);
    }
}