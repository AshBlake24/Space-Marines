using System;
using System.Collections.Generic;
using Roguelike.Audio.Factory;
using Roguelike.Audio.Service;
using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Infrastructure.Services.SaveLoad;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic;
using Roguelike.Logic.Pause;

namespace Roguelike.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, AllServices services,
            ICoroutineRunner coroutineRunner)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services, coroutineRunner),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingScreen,
                    services.Single<IGameFactory>(), services.Single<ISaveLoadService>(),
                    services.Single<IUIFactory>(), services.Single<IAudioFactory>(), services.Single<IWindowService>()),

                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistentDataService>(),
                    services.Single<ISaveLoadService>(), services.Single<IWeaponFactory>(),
                    services.Single<IStaticDataService>(), services.Single<IAudioService>()),

                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<ISaveLoadService>(), 
                    services.Single<ITimeService>(), services.Single<IPersistentDataService>()),
                
                [typeof(ExitGameState)] = new ExitGameState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            IPayloadedState<TPayload> state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}