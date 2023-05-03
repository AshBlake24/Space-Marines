namespace Roguelike.UI.Windows
{
    public class ReturnCharacterSelection : ConfirmationWindow
    {
        protected override void OnConfirm() =>
            SceneLoadingService.Load(StaticData.GameConfig.StartPlayerLevel);
    }
}