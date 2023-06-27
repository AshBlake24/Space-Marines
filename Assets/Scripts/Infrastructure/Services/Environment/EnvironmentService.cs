using UnityEngine;

namespace Roguelike.Infrastructure.Services.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly EnvironmentType _environment;

        public EnvironmentService()
        {
            Debug.Log(SystemInfo.deviceType);
#if UNITY_WEBGL && !UNITY_EDITOR
            _environment = SystemInfo.deviceType == DeviceType.Desktop 
                ? EnvironmentType.Desktop 
                : EnvironmentType.Mobile;
#else
            _environment = EnvironmentType.Desktop;
#endif
        }

        public EnvironmentType GetDeviceType() =>
            _environment;
    }
}