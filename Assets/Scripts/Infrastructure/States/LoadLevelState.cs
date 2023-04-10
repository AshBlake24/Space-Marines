using Roguelike.Infrastructure.Factory;
using Roguelike.Logic;
using Roguelike.Logic.Camera;
using UnityEngine;

namespace Roguelike.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string InitialPointTag = "InitialPoint";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingScreen;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingScreen = loadingScreen;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _loadingScreen.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingScreen.Hide();
        }

        private void OnLoaded()
        {
            _gameFactory.GenerateLevel();

            GameObject initialPoint = GameObject.FindWithTag(InitialPointTag);
            GameObject player = _gameFactory.CreatePlayer(initialPoint.transform);
            
            CameraFollow(player);
            
            CameraFollow(player);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void CameraFollow(GameObject hero)
        { 
            Camera.main
                .GetComponent<CameraFollower>()
                .Follow(hero);
        }
        
        private void CameraFollow(GameObject hero)
        { 
            Camera.main
                .GetComponent<CameraFollower>()
                .Follow(hero);
        }
    }
}