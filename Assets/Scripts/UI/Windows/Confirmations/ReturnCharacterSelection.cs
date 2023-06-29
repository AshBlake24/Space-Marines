namespace Roguelike.UI.Windows.Confirmations
{
    public class ReturnCharacterSelection : ConfirmationWindow
    {
        protected override void OnConfirm() =>
            SceneLoadingService.Load(StaticData.GameConfig.StartLevel);
    }
}