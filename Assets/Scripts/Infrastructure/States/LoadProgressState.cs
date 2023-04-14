using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;

namespace Roguelike.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private const string StartLevel = "Hub";
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentDataService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgress();
            
            _stateMachine.Enter<LoadLevelState, string>(_progressService.PlayerProgress.WorldData.CurrentLevel);
        }

        public void Exit()
        {
        }

        private void LoadProgress() => 
            _progressService.PlayerProgress =
                _saveLoadService.LoadProgress()
                ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress() => 
            new PlayerProgress(StartLevel);
    }
}