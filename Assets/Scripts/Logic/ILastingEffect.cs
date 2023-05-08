using Roguelike.Infrastructure;

namespace Roguelike.Logic
{
    public interface ILastingEffect
    {
        void Construct(ICoroutineRunner coroutineRunner);
    }
}