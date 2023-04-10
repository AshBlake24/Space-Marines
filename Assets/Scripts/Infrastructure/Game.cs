using Roguelike.Infrastructure.States;
using Roguelike.Logic;
using Roguelike.Services.Input;

namespace Roguelike.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen);
        }
    }
}