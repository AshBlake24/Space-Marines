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

        public void ShowVideoAd(Action onRewardedCallback = null)
        {
            if (YandexGamesSdk.IsInitialized)
                VideoAd.Show(OnVideoOpen, onRewardedCallback, OnVideoClose);
        }

        private void OnVideoOpen()
        {
            IsVideoOpen = true;
            PauseGame();
            MuteAudio();
        }

        private void OnVideoClose()
        {
            IsVideoOpen = false;
            ResumeGame();
            UnmuteAudio();
        }

        private void PauseGame() =>
            _timeService.PauseGame();

        private void ResumeGame() =>
            _timeService.ResumeGame();

        private void MuteAudio() =>
            _audioService.SetChannelMute(AudioChannel.Master, true);

        private void UnmuteAudio() =>
            _audioService.SetChannelMute(AudioChannel.Master, false);
    }
}