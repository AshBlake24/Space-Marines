using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Logic.Pause;
using Roguelike.StaticData.Levels;

namespace Roguelike.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ITimeService _timeService;
        private readonly IPersistentDataService _persistentData;
        private readonly IStaticDataService _staticDataService;

        public GameLoopState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService,
            ITimeService timeService, IPersistentDataService persistentData, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _timeService = timeService;
            _persistentData = persistentData;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            if (_timeService.IsPaused)
                _timeService.ResumeGame();
        }

        public void Exit()
        {
            if (_persistentData.IsResetting)
            {
                _persistentData.PlayerProgress = new PlayerProgress(
                    _staticDataService.GameConfig.StartLevel);
            }

            _saveLoadService.SaveProgress();
        }
    }
}