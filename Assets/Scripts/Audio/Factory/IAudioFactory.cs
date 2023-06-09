using Roguelike.Infrastructure.Services;
using UnityEngine;

namespace Roguelike.Audio.Factory
{
    public interface IAudioFactory : IService
    {
        AudioSource CreateAudioSource();
    }
}