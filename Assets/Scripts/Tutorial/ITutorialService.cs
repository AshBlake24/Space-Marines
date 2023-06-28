using Roguelike.Infrastructure.Factory;
using Roguelike.Infrastructure.Services;
using Roguelike.Localization;

namespace Roguelike.Tutorial
{
    public interface ITutorialService : IService
    {
        void GetUIFactory(IUIFactory uiFactory);
        void TryShowTutorialWindow(LocalizedString text);
    }
}