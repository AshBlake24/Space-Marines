using System;
using Roguelike.Audio.Factory;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic;
using Roguelike.StaticData.Levels;
using Roguelike.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Roguelike.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelId>
    {
        private const string InitialPointTag = "InitialPoint";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IUIFactory _uiFactory;
        private readonly IAudioFactory _audioFactory;
        private readonly IWindowService _windowService;

        private LevelId _activeScene;

        public LoadLevelState(
            GameStateMachine stateMachine,
            SceneLoader sceneLoader,
            LoadingScreen loadingScreen,
            IGameFactory gameFactory,
            ISaveLoadService saveLoadService,
            IUIFactory uiFactory,
            IAudioFactory audioFactory,
            IWindowService windowService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
            _uiFactory = uiFactory;
            _audioFactory = audioFactory;
            _windowService = windowService;
        }

        public void Enter(LevelId level)
        {
            _loadingScreen.Show();

            Cleanup();

            _sceneLoader.Load(level.ToString(), OnLoaded);
        }

        public void Exit() =>
            _loadingScreen.Hide();

        private void OnLoaded()
        {
            Helpers.InitializePools();

            InitUIRoot();
            InitAudioRoot();
            InitCurrentLevel();
            InformProgressReaders();

            _stateMachine.Enter<GameLoopState>();
        }

        private void InitCurrentLevel()
        {
            Enum.TryParse<LevelId>(SceneManager.GetActiveScene().name, true, out _activeScene);

            switch (_activeScene)
            {
                case LevelId.MainMenu:
                    InitMainMenu();

                    break;
                case LevelId.Hub:
                    InitHub();

                    break;
                case LevelId.Dungeon:
                    InitDungeon();

                    break;
                case LevelId.Test:
                    InitWorld();
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            new GameObject("AppCloser").AddComponent<AppCloser>()
                .Construct(_stateMachine);
        }

        private void InitAudioRoot() =>
            _audioFactory.CreateAudioRoot();

        private void InitUIRoot() =>
            _uiFactory.CreateUIRoot();

        private void InitMainMenu() =>
            _windowService.Open(WindowId.MainMenu);

        private void InitHub() =>
            _gameFactory.CreateCharacterSelectionMode();

        private void InitDungeon()
        {
            _gameFactory.GenerateLevel();
            InitWorld();
        }

        private void InitWorld()
        {
            GameObject player = InitPlayer();

            InitHud(player, createMiniMap: true);
            InitCamera(player);
        }

        private GameObject InitPlayer()
        {
            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint.transform);

            return player;
        }

        private void InitHud(GameObject player, bool createMiniMap) =>
            _gameFactory.CreateHud(player, createMiniMap);

        private void InitCamera(GameObject player) => 
            _gameFactory.CreatePlayerCamera(player);

        private void InformProgressReaders() =>
            _saveLoadService.InformProgressReaders();

        private void Cleanup()
        {
            _saveLoadService.Cleanup();
            Helpers.CleanupPools();
        }
    }
}