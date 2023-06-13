using Roguelike.Infrastructure.Services.Input;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic.Pause;
using UnityEngine;

namespace Roguelike.Player.Observers
{
    public class PlayerPauseObserver : MonoBehaviour
    {
        private IWindowService _windowService;
        private IInputService _inputService;
        private ITimeService _timeService;

        public void Construct(IWindowService windowService, IInputService inputService, ITimeService timeService)
        {
            _windowService = windowService;
            _inputService = inputService;
            _timeService = timeService;
            _inputService.PausePressed += OnPausePressed;
        }

        private void OnDestroy() =>
            _inputService.PausePressed -= OnPausePressed;

        private void OnPausePressed()
        {
            if (_timeService.IsPaused == false && _timeService.PauseMenu == null)
                _windowService.Open(WindowId.PauseMenu);
            else if (_timeService.PauseMenu != null)
                _timeService.PauseMenu.Close();
        }
    }
}