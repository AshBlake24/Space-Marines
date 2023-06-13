using System;
using Roguelike.Infrastructure.Services.StaticData;
using Roguelike.StaticData.Levels;
using UnityEngine;

namespace Roguelike.StaticData.Audio
{
    [Serializable]
    public class MusicConfig : IStaticData
    {
        public MusicId LevelId;
        public AudioClip Music;

        public Enum Key => LevelId;
    }
}