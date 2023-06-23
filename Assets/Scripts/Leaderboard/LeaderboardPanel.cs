using Agava.YandexGames;
using UnityEngine;

namespace Roguelike.Leaderboard
{
    public class LeaderboardPanel : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboardView;
        [SerializeField] private GameObject _authorizationPanel;
        
        private void OnEnable()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            EnableAuthorizationPanel();
#else
            if (PlayerAccount.IsAuthorized)
                EnableLeaderboard();
            else
                EnableAuthorizationPanel();
#endif
        }

        private void EnableLeaderboard()
        {
            _authorizationPanel.gameObject.SetActive(false);
            _leaderboardView.gameObject.SetActive(true);
            _leaderboardView.InitLeaderboard();
        }

        private void EnableAuthorizationPanel()
        {
            _authorizationPanel.gameObject.SetActive(true);
            _leaderboardView.gameObject.SetActive(false);
        }
    }
}