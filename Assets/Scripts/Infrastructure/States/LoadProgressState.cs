using Agava.YandexGames;
using Roguelike.Audio.Service;
using Roguelike.Data;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;

namespace Roguelike.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentDataService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;
        private readonly IAudioService _audioService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistentDataService progressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService, IAudioService audioService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
            _audioService = audioService;
        }

        public void Enter()
        {
            LoadProgress();
        }

        public void Exit()
        {
        }

        private void LoadProgress()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (PlayerAccount.IsAuthorized)
                _saveLoadService.LoadProgressFromCloud(OnDataLoaded);
            else
                OnDataLoaded(_saveLoadService.LoadProgressFromPrefs());
#else
            OnDataLoaded(_saveLoadService.LoadProgressFromPrefs());
#endif
        }

        private void OnDataLoaded(PlayerProgress data)
        {
            _progressService.PlayerProgress = data ?? CreateNewProgress();
            _audioService.LoadVolumeSettings();
            _stateMachine.Enter<LoadLevelState, LevelId>(_staticDataService.GameConfig.StartScene);
        }

        private PlayerProgress CreateNewProgress() =>
            new(_staticDataService.GameConfig.StartLevel);
    }
}