namespace Roguelike.Infrastructure.Services.Random
{
    public interface IRandomService : IService
    {
        int Next(int min, int max);
        float Next(float min, float max);
    }
}