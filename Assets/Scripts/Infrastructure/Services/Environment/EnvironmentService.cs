using Agava.YandexGames;

namespace Roguelike.Infrastructure.Services.Environment
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly EnvironmentType _environment;

        public EnvironmentService()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            _environment = Device.Type == DeviceType.Desktop 
                ? EnvironmentType.Desktop 
                : EnvironmentType.Mobile;
#else
            _environment = EnvironmentType.Mobile;
#endif
        }

        public EnvironmentType GetDeviceType() =>
            _environment;
    }
}