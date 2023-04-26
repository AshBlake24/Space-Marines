using Roguelike.Infrastructure.Services;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        void CreatePauseMenu();
        void CreateUIRoot();
    }
}