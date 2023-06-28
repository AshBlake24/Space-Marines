using Device = Agava.WebUtility.Device;

namespace Roguelike.Infrastructure.Services.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly EnvironmentType _environment;

        public EnvironmentService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _environment = Device.IsMobile 
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