using Roguelike.Infrastructure.Services;

namespace Roguelike.Audio
{
    public interface IAudioService : IService
    {
        // void MuteChannel(AudioChannel channel);
        // void UnmuteChannel(AudioChannel channel);
        void SetChannelVolume(AudioChannel channel, float value);
        void LoadVolumeSettings();
    }
}