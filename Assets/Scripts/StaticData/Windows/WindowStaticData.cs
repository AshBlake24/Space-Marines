using System.Collections.Generic;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Tutorials;
using UnityEngine;

namespace Roguelike.StaticData.Windows
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/Window Static Data")]
    public class WindowStaticData : ScriptableObject
    {
        public List<WindowConfig<WindowId>> WindowConfigs;
        public List<WindowConfig<TutorialId>> TutorialConfigs;
    }
}