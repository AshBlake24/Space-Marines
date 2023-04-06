namespace Roguelike.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine();
        }
    }
}