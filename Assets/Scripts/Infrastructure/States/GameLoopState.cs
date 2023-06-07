using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Logic.Pause;

namespace Roguelike.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ITimeService _timeService;

        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, 
            ITimeService timeService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _timeService = timeService;
        }

        public void Enter()
        {
            if (_timeService.IsPaused)
                _timeService.ResumeGame();
        }

        public void Exit()=> 
            _saveLoadService.SaveProgress();
    }
}