namespace Roguelike.Enemies.Transitions
{
    public class AppearanceFinished : Transition
    {
        public void FinishAppearance()
        {
            NeedTransit?.Invoke(targetState);
        }
    }
}