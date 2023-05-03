namespace Roguelike.UI.Windows
{
    public class ReturnHomeWindow : ConfirmationWindow
    {
        protected override void OnConfirm() =>
            SceneLoadingService.Load(StaticData.GameConfig.StartScene);
    }
}