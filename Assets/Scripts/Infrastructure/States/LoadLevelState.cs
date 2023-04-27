using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.Environment;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Logic;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using Roguelike.UI.Elements;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentDataService _progressService;
        private readonly IEnvironmentService _environmentService;
        private readonly IUIFactory _uiFactory;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            ISaveLoadService saveLoadService,
            IPersistentDataService progressService,
            IEnvironmentService environmentService,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _environmentService = environmentService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _saveLoadService.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            InitUIRoot();
            InitGameWorld();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitGameWorld()
        {
            if (SceneManager.GetActiveScene().name == LevelId.Dungeon.ToString())
            {
                _gameFactory.GenerateLevel(_stateMachine);
                
                GameObject player = InitPlayer();
                
                InitHud(player);
                CameraFollow(player);
            }
        }

        private GameObject InitPlayer()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint.transform);

            return player;
        }

        private void InitHud(GameObject player)
        {
            EnvironmentType deviceType = _environmentService.GetDeviceType();

            GameObject hud = _gameFactory.CreateHud(deviceType);

            hud.GetComponentInChildren<AmmoCounter>()
                .Construct(player.GetComponent<PlayerShooter>());

            hud.GetComponentInChildren<ActorUI>()
                .Construct(player.GetComponent<PlayerHealth>());
        }

        private void InformProgressReaders()
        {
            foreach (IProgressReader progressReader in _saveLoadService.ProgressReaders)
                progressReader.ReadProgress(_progressService.PlayerProgress);
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main
                .GetComponent<CameraFollower>()
                .Follow(hero);
        }
    }
}