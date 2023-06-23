using System;

namespace Roguelike.Data
{
    [Serializable]
    public class CurrentKillData
    {
        public int KilledMonsters;
        public int KilledBosses;

        public void Reset()
        {
            KilledMonsters = 0;
            KilledBosses = 0;
        }

        public void OnMonsterKilled() => KilledMonsters++;
        public void OnBossKilled() => KilledBosses++;
    }
}