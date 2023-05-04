using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Player;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Windows
{
    public interface IWindowService : IService
    {
        BaseWindow Open(WindowId windowId);
        void OpenResurrectionWindow(PlayerDeath playerDeath);
    }
}