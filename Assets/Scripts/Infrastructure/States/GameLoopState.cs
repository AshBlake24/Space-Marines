using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Logic.Pause;

namespace Roguelike.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ITimeService _timeService;
        private readonly IPersistentDataService _persistentData;

        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService,
            ITimeService timeService, IPersistentDataService persistentData)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _timeService = timeService;
            _persistentData = persistentData;
        }

        public void Enter()
        {
            if (_timeService.IsPaused)
                _timeService.ResumeGame();
        }

        public void Exit()
        {
            if (_persistentData.IsResetting == false)
                _saveLoadService.SaveProgress();
        }
    }
}