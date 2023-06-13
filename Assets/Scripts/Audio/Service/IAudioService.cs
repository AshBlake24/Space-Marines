using Roguelike.Infrastructure.Services;

namespace Roguelike.Audio.Service
{
    public interface IAudioService : IService
    {
        float GetChannelVolumeLinear(AudioChannel channel);
        bool IsChannelMuted(AudioChannel channel);
        void SetChannelVolume(AudioChannel channel, float value);
        void SetChannelMute(AudioChannel channel, bool isMuted);
        void LoadVolumeSettings();
    }
}