using System;
using Roguelike.StaticData.Enhancements;

namespace Roguelike.Data
{
    [Serializable]
    public class EnhancementUsageData
    {
        public EnhancementId Id;
        public int UsedTimes;

        public EnhancementUsageData(EnhancementId enhancementId)
        {
            Id = enhancementId;
            UsedTimes = 1;
        }
    }
}