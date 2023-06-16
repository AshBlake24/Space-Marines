using System;

namespace Roguelike.Data
{
    [Serializable]
    public class CurrentKillData
    {
        public int Kills;

        public void Reset() => Kills = 0;
        
        public void AddKill() => Kills++;
    }
}