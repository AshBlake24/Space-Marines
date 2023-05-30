namespace Roguelike.Player.Enhancements
{
    public abstract class Enhancement : IEnhancement
    {
        protected Enhancement()
        {
            CurrentTier = 0;
        }
        
        public int CurrentTier { get; protected set; }
        public abstract bool CanUpgrade { get; }

        public abstract void Upgrade();

        public abstract void Cleanup();
    }
}