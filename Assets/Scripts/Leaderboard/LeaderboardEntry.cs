using System;
using Agava.YandexGames;

namespace Roguelike.Leaderboard
{
    public class LeaderboardEntry
    {
        private const string Anonymous = nameof(Anonymous);
        
        public LeaderboardEntry(LeaderboardEntryResponse entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            
            SetName(entry.player.publicName);
            AvatarUrl = entry.player.profilePicture;
            Score = entry.score.ToString();
            Rank = $"{entry.rank}.";
        }

        public string PlayerName { get; private set; }
        public string AvatarUrl { get; private set; }
        public string Score { get; private set; }
        public string Rank { get; private set; }

        private void SetName(string name)
        {
            PlayerName = name;

            if (string.IsNullOrEmpty(PlayerName))
                PlayerName = Anonymous;
        }
    }
}