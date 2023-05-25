using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.UI.Windows;

namespace Roguelike.StaticData.Windows
{
    [Serializable]
    public class WindowConfig : IStaticData
    {
        public WindowId WindowId;
        public BaseWindow WindowPrefab;
    }
}