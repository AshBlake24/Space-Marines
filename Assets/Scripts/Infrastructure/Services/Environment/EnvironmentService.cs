using UnityEngine;

namespace Roguelike.Infrastructure.Services.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly EnvironmentType _environment;

        public EnvironmentService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _environment = Application.isMobilePlatform
                ? EnvironmentType.Mobile 
                : EnvironmentType.Desktop;
#else
            _environment = EnvironmentType.Desktop;
#endif
        }

        public EnvironmentType GetDeviceType() =>
            _environment;
    }
}