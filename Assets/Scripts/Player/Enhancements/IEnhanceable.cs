namespace Roguelike.Player.Enhancements
{
    public interface IEnhanceable<in TPayload>
    {
        void Enhance(TPayload payload);
    }
}