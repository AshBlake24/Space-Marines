namespace Roguelike.Enemies.Transitions
{
    public class TargetOnLineOfSight : TargetIsNotOnLineOfSight
    {
        protected override void CheackLineOfSight()
        {
            if (_agent.path.corners.Length == MinCornersCount)
                NeedTransit?.Invoke(targetState);
        }
    }
}