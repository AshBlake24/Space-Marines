using Agava.YandexGames;
using Roguelike.Infrastructure.AssetManagement;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Input;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;

namespace Roguelike.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialScene = "Initial";
        private const string DesktopEnvironment = "Desktop";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialScene, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            _services.RegisterSingle<IInputService>(GetInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            _services.RegisterSingle<IPersistentDataService>(new PersistentDataService());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentDataService>()));
            _services.RegisterSingle<IWeaponFactory>(new WeaponFactory(_services.Single<IStaticDataService>(), _services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IPersistentDataService>(), _services.Single<ISaveLoadService>(), _services.Single<IWeaponFactory>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticDataService = new StaticDataService();
            staticDataService.LoadWeapons();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
        }

        private IInputService GetInputService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            if (YandexGamesSdk.Environment == DesktopEnvironment)
                return new DesktopInputService();
            else
                return new MobileInputService();
#else
            return new DesktopInputService();
#endif
        }
    }
}