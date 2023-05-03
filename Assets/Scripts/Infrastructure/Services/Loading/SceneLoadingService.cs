using Roguelike.Infrastructure.States;
using Roguelike.StaticData.Levels;

namespace Roguelike.Infrastructure.Services.Loading
{
    public class SceneLoadingService : ISceneLoadingService
    {
        private readonly GameStateMachine _gameStateMachine;

        public SceneLoadingService(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Load(LevelId level) => 
            _gameStateMachine.Enter<LoadLevelState, LevelId>(level);
    }
}