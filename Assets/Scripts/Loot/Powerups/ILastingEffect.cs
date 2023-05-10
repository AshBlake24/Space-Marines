using Roguelike.Infrastructure;

namespace Roguelike.Loot.Powerups
{
    public interface ILastingEffect
    {
        float Duration { get; }
        void Construct(ICoroutineRunner coroutineRunner);
    }
}