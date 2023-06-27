using System;
using Agava.YandexGames;
using Roguelike.Audio.Service;

namespace Roguelike.Ads
{
    public class AdsService : IAdsService
    {
        private readonly IAudioService _audioService;

        public AdsService(IAudioService audioService)
        {
            _audioService = audioService;
        }

        public bool IsVideoOpen { get; private set; }
        

        public void ShowVideoAd(Action onRewardedCallback = null)
        {
            if (YandexGamesSdk.IsInitialized){}
                VideoAd.Show(OnVideoOpen, onRewardedCallback, OnVideoClose);
        }

        private void OnVideoOpen()
        {
            IsVideoOpen = true;
            MuteAudio();
        }

        private void OnVideoClose()
        {
            IsVideoOpen = false;
            UnmuteAudio();
        }

        private void MuteAudio() => 
            _audioService.SetChannelMute(AudioChannel.Master, true);

        private void UnmuteAudio() => 
            _audioService.SetChannelMute(AudioChannel.Master, false);
    }
}