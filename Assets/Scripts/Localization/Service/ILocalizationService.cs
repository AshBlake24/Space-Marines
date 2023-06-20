using Roguelike.Infrastructure.Services;

namespace Roguelike.Localization.Service
{
    public interface ILocalizationService : IService
    {
        void Init();
    }
}