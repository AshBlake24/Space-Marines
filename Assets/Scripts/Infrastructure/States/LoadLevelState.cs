using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Logic;
using Roguelike.StaticData.Levels;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using CameraFollower = Roguelike.Logic.CameraFollower;

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
        private readonly IUIFactory _uiFactory;

        private string _activeSceneName;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            ISaveLoadService saveLoadService,
            IPersistentDataService progressService,
            IUIFactory uiFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _uiFactory = uiFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();

            Cleanup();

            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            _activeSceneName = SceneManager.GetActiveScene().name;

            InitUIRoot();
            Helpers.InitializePools();

            if (_activeSceneName == LevelId.Dungeon.ToString())
                InitDungeon();
            else if (_activeSceneName == LevelId.Hub.ToString())
                InitHub();

            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitDungeon()
        {
            _gameFactory.GenerateLevel(_stateMachine);

            GameObject player = InitPlayer();

            InitHud(player);
            CameraFollow(player);
        }

        private void InitHub() =>
            _gameFactory.CreateCharacterSelectionMode();

        private void InitHud(GameObject player) =>
            _gameFactory.CreateHud(player);

        private GameObject InitPlayer()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint.transform);

            return player;
        }

        private void CameraFollow(GameObject hero)
        {
            Camera.main
                .GetComponent<CameraFollower>()
                .Follow(hero);
        }

        private void Cleanup()
        {
            _saveLoadService.Cleanup();
            Helpers.CleanupPools();
        }

        private void InformProgressReaders() => 
            _saveLoadService.InformProgressReaders();
    }
}