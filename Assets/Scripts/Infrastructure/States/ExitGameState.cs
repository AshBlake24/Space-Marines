namespace Roguelike.Infrastructure.States
{
    public class ExitGameState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public ExitGameState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}