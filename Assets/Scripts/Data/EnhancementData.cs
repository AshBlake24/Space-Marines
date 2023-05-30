using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Data
{
    [Serializable]
    public class EnhancementData
    {
        public EnhancementId Id;
        public int Tier;
        public int Value;

        public event Action Changed;
    }
}