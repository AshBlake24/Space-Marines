using Roguelike.Infrastructure.Services.SaveLoad;

namespace Roguelike.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
        }

        public void Exit()=> 
            _saveLoadService.SaveProgress();
    }
}