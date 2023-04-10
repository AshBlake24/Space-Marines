using Agava.YandexGames;
using Roguelike.Services.Input;

namespace Roguelike.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadLevelState, string>("Test01");

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }

        private IInputService RegisterInputService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (YandexGamesSdk.Environment == "Desktop")
                return new DesktopInputService();
            else
                return new MobileInputService();
#else
            return new DesktopInputService();
#endif
        }
    }
}