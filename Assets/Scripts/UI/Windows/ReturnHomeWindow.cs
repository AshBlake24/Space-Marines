using Roguelike.Infrastructure.Services.Loading;
using Roguelike.Infrastructure.Services.StaticData;

namespace Roguelike.UI.Windows
{
    public class ReturnHomeWindow : ConfirmationWindow
    {
        private IStaticDataService _staticData;
        private ISceneLoadingService _sceneLoadingService;

        public void Construct(IStaticDataService staticData, ISceneLoadingService sceneLoadingService)
        {
            _staticData = staticData;
            _sceneLoadingService = sceneLoadingService;
        }

        protected override void OnConfirm() =>
            _sceneLoadingService.Load(_staticData.GameConfig.MainMenuScene);
    }
}