using System;
using Agava.YandexGames;

namespace Roguelike.Leaderboard
{
    public class LeaderboardEntry
    {
        private const string Anonymous = nameof(Anonymous);

        public string PlayerName { get; private set; }
        public string Score { get; private set; }
        public string Rank { get; private set; }

        public LeaderboardEntry(LeaderboardEntryResponse entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            
            //todo : get avatar

            SetName(entry.player.publicName);
            Score = entry.score.ToString();
            Rank = $"{entry.rank}.";
        }

        private void SetName(string name)
        {
            PlayerName = name;

            if (string.IsNullOrEmpty(PlayerName))
                PlayerName = Anonymous;
        }
    }
}