using Roguelike.StaticData.Levels;

namespace Roguelike.Infrastructure.Services.Loading
{
    public interface ISceneLoadingService : IService
    {
        void Load(LevelId level);
    }
}