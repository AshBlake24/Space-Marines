using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Player.Enhancements
{
    public class EnhancementsObserver : MonoBehaviour
    {
        private IPersistentDataService _progressService;
        private PlayerEnhancements _playerEnhancements;

        public void Construct(IPersistentDataService progressService, PlayerEnhancements playerEnhancements)
        {
            _progressService = progressService;
            _playerEnhancements = playerEnhancements;
            
            _playerEnhancements.Updated += OnUpdated;
        }

        private void OnDestroy() => 
            _playerEnhancements.Updated -= OnUpdated;

        private void OnUpdated() => 
            _playerEnhancements.WriteProgress(_progressService.PlayerProgress);
    }
}