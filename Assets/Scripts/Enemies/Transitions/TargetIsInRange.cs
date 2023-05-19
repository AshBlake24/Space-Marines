namespace Roguelike.Enemies.Transitions
{
    public class TargetIsInRange : TargetIsNotInRange
    {
        protected override void CheackLineOfSight()
        {
            if (_agent.path.corners.Length == MinCornersCount)
                NeedTransit?.Invoke(targetState);
        }
    }
}