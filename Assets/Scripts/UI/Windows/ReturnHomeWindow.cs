using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.States;

namespace Roguelike.UI.Windows
{
    public class ReturnHomeWindow : ConfirmationWindow
    {
        private GameStateMachine _stateMachine;
        private IStaticDataService _staticData;

        public void Construct(GameStateMachine stateMachine, IStaticDataService staticData)
        {
            _stateMachine = stateMachine;
            _staticData = staticData;
        }
        
        protected override void OnConfirm()
        {
            _stateMachine.Enter<LoadLevelState, string>(
                _staticData.GameConfig.MainMenuScene.ToString());
        }
    }
}