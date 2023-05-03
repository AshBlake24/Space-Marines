using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.States;
using Roguelike.StaticData.Levels;

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
            _stateMachine.Enter<LoadLevelState, LevelId>(
                _staticData.GameConfig.MainMenuScene);
        }
    }
}