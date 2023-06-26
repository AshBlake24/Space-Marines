using System;
using UnityEngine;

namespace Roguelike.StaticData.Enhancements
{
    [Serializable]
    public class TierInfo
    {
        public int Value;
        public int Price;
        public ParticleSystem PlayerEffect;
    }
}