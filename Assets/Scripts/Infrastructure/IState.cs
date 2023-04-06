namespace Roguelike.Infrastructure
{
    public interface IState
    {
        void Enter();
        void Exit();
    }
}