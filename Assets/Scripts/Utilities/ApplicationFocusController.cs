using Roguelike.Ads;
using Roguelike.Audio.Service;
using Roguelike.Logic.Pause;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ApplicationFocusController : MonoBehaviour
    {
        private IAudioService _audioService;
        private ITimeService _timeService;
        private IAdsService _adsService;

        public void Construct(IAudioService audioService, ITimeService timeService, IAdsService adsService)
        {
            _audioService = audioService;
            _timeService = timeService;
            _adsService = adsService;
        }

        public void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus == false)
                OnFocusLost();
            else
                OnFocusReturn();
        }

        private void OnFocusLost()
        {
            _timeService.PauseGame();
            _audioService.SetChannelMute(AudioChannel.Master, true);
        }

        private void OnFocusReturn()
        {
            if (_adsService.IsVideoOpen == false)
            {
                _timeService.ResumeGame();
                _audioService.SetChannelMute(AudioChannel.Master, false);
            }
        }
    }
}