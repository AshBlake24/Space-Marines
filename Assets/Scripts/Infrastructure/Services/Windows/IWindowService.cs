using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.UI.Windows;
using UnityEngine;

namespace Roguelike.Infrastructure.Services.Windows
{
    public interface IWindowService : IService
    {
        BaseWindow Open(WindowId windowId);
    }
}