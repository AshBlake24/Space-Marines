using Roguelike.Audio.Logic;
using Roguelike.Infrastructure.Services;

namespace Roguelike.Audio.Factory
{
    public interface IAudioFactory : IService
    {
        Sound CreateAudioSource();
        void CreateAudioRoot();
    }
}