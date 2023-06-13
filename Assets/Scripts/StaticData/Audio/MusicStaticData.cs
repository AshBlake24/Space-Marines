using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.StaticData.Audio
{
    [CreateAssetMenu(fileName = "MusicStaticData", menuName = "Static Data/Audio/Music Static Data")]
    public class MusicStaticData : ScriptableObject
    {
        public List<MusicConfig> Configs;
    }
}