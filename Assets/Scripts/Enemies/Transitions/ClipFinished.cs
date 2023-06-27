namespace Roguelike.Enemies.Transitions
{
    public class ClipFinished : Transition
    {
        public void FinishState()
        {
            NeedTransit?.Invoke(targetState);
        }
    }
}