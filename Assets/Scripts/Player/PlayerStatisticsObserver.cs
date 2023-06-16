using Roguelike.Data;
using UnityEngine;

namespace Roguelike.Player
{
    public class PlayerStatisticsObserver : MonoBehaviour
    {
        private PlayerProgress _progressService;

        public void Construct(PlayerProgress progressService)
        {
            _progressService = progressService;
        }

        public void OnChestOpened() => 
            _progressService.Statistics.ChestsOpened++;
    }
}