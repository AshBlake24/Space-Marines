namespace Roguelike.Infrastructure.Services.Random
{
    public class UnityRandomService : IRandomService
    {
        public int Next(int min, int max) =>
            UnityEngine.Random.Range(min, max + 1);

        public float Next(float min, float max) => 
            UnityEngine.Random.Range(min, max);
    }
}