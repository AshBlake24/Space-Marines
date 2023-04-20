namespace Roguelike.Infrastructure.Services.Environment
{
    public interface IEnvironmentService : IService
    {
        EnvironmentType GetDeviceType();
    }
}