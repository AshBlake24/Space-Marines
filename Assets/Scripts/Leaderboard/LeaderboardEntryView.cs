using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private Image _background;
        [SerializeField] private Color _evenColor;
        [SerializeField] private Color _oddColor;
        
        [Header("Player Info")]
        [SerializeField] private Image _avatar;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private TextMeshProUGUI _score;
    }
}