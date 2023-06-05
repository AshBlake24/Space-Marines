using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Weapons.Logic
{
    public class WeaponsObserver : MonoBehaviour
    {
        private IPersistentDataService _progressService;
        private PlayerShooter _playerShooter;

        public void Construct(IPersistentDataService progressService, PlayerShooter playerShooter)
        {
            _progressService = progressService;
            _playerShooter = playerShooter;
            
            _playerShooter.DroppedWeapon += OnDroppedWeapon;
        }
        
        private void OnDestroy() => 
            _playerShooter.DroppedWeapon -= OnDroppedWeapon;

        private void OnDroppedWeapon(IWeapon weapon) => 
            weapon.WriteProgress(_progressService.PlayerProgress);   
    }
}