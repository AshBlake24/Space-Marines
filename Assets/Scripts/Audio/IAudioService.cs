using Roguelike.Infrastructure.Services;

namespace Roguelike.Audio
{
    public interface IAudioService : IService
    {
        void SetChannelVolume(AudioChannel channel, float value);
        void LoadVolumeSettings();
        float GetChannelVolumeLinear(AudioChannel channel);
    }
}