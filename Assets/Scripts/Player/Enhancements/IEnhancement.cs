namespace Roguelike.Player.Enhancements
{
    public interface IEnhancement
    {
        int CurrentTier { get; }
        bool CanUpgrade { get; }
        
        void Upgrade();
    }
}