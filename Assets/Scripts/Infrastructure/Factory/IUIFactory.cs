using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;

namespace Roguelike.Infrastructure.Factory
{
    public interface IUIFactory : IService
    {
        void CreateWindow(IWindowService windowService, WindowId windowId);
        void CreateUIRoot();
    }
}