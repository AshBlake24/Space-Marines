using Agava.YandexGames;
using UnityEngine;

namespace Roguelike.Leaderboard
{
    public class LeaderboardView : MonoBehaviour
    {
        private const string LeaderboardName = "Leaderboard";

        [SerializeField] private LeaderboardEntryView[] _entries;

        public void InitLeaderboard()
        {
            LoadEntries();
            LoadPlayerEntry();
        }
        
        private void LoadEntries()
        {
            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, OnGetEntries, null, _entries.Length - 1);
        }

        private void LoadPlayerEntry()
        {
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                LeaderboardEntry leaderboardEntry = new(result);
                _entries[^1].SetData(leaderboardEntry);
            });
        }

        private void OnGetEntries(LeaderboardGetEntriesResponse response)
        {
            for (int i = 0; i < response.entries.Length; i++)
            {
                LeaderboardEntry leaderboardEntry = new(response.entries[i]);
                _entries[i].SetData(leaderboardEntry);
            }
        }
    }
}