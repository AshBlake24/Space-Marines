using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [Header("Player Info")]
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private TextMeshProUGUI _score;
        
        public void SetData(LeaderboardEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            _rank.text = entry.Rank;
            _name.text = entry.PlayerName;
            _score.text = entry.Score;
        }
    }
}