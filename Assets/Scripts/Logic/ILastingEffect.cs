using Roguelike.Infrastructure;

namespace Roguelike.Logic
{
    public interface ILastingEffect
    {
        float Duration { get; }
        void Construct(ICoroutineRunner coroutineRunner);
    }
}