using System;
using Agava.YandexGames;
using Roguelike.Audio.Service;
using Roguelike.Logic.Pause;

namespace Roguelike.Ads
{
    public class AdsService : IAdsService
    {
        private readonly IAudioService _audioService;
        private readonly ITimeService _timeService;

        public AdsService(IAudioService audioService, ITimeService timeService)
        {
            _audioService = audioService;
            _timeService = timeService;
        }

        public bool IsVideoOpen { get; private set; }

        public void ShowInterstitialAd()
        {
            if (YandexGamesSdk.IsInitialized) 
                InterstitialAd.Show(OnAdsOpen, OnInterstitialClose);
        }

        public void ShowVideoAd(Action onRewardedCallback = null)
        {
            if (YandexGamesSdk.IsInitialized)
                VideoAd.Show(OnVideoOpen, onRewardedCallback, OnVideoClose);
        }

        private void OnAdsOpen()
        {
            _timeService.PauseGame();
            _audioService.SetChannelMute(AudioChannel.Master, true);
        }

        private void OnAdsClose()
        {
            _timeService.ResumeGame();
            _audioService.SetChannelMute(AudioChannel.Master, false);
        }

        private void OnVideoOpen()
        {
            IsVideoOpen = true;
            OnAdsOpen();
        }

        private void OnVideoClose()
        {
            IsVideoOpen = false;
            OnAdsClose();
        }

        private void OnInterstitialClose(bool wasShown) => OnAdsClose();
    }
}