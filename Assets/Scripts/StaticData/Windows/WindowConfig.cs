using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.UI.Windows;

namespace Roguelike.StaticData.Windows
{
    [Serializable]
    public class WindowConfig<TKey> : IStaticData where TKey : Enum
    {
        public TKey Id;
        public BaseWindow WindowPrefab;
        
        public Enum Key => Id;
    }
}