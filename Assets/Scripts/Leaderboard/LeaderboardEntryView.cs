using System;
using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Leaderboard
{
    public class LeaderboardEntryView : MonoBehaviour
    {
        [Header("Player Info")]
        [SerializeField] private RawImage _avatar;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _rank;
        [SerializeField] private TextMeshProUGUI _score;

        private bool _avatarDownloaded;

        public void SetData(LeaderboardEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));

            DownloadAvatar(entry.AvatarUrl);
            _avatar.enabled = false;
            _rank.text = entry.Rank;
            _name.text = entry.PlayerName;
            _score.text = entry.Score;
        }

        private void DownloadAvatar(string avatarUrl)
        {
            if (_avatarDownloaded == false)
            {
                RemoteImage remoteImage = new(avatarUrl);
                remoteImage.Download(SetAvatar);
            }
        }

        private void SetAvatar(Texture2D texture)
        {
            _avatar.enabled = true;
            _avatar.texture = texture;
            _avatarDownloaded = true;
        }
    }
}