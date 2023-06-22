using System;
using Roguelike.StaticData.Characters;

namespace Roguelike.Data
{
    [Serializable]
    public class CharacterUsageData
    {
        public CharacterId Id;
        public int UsedTimes;

        public CharacterUsageData(CharacterId characterId)
        {
            Id = characterId;
            UsedTimes = 1;
        }
    }
}