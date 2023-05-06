using Roguelike.Infrastructure.Services.PersistentData;
using Roguelike.Player;
using UnityEngine;

namespace Roguelike.Weapons.Logic
{
    public class WeaponsObserver : MonoBehaviour
    {
        private IPersistentDataService _persistentData;
        private PlayerShooter _playerShooter;

        public void Construct(IPersistentDataService persistentData, PlayerShooter playerShooter)
        {
            _persistentData = persistentData;
            _playerShooter = playerShooter;
            
            _playerShooter.DroppingWeapon += OnDroppingWeapon;
        }
        
        private void OnDestroy() => 
            _playerShooter.DroppingWeapon -= OnDroppingWeapon;

        private void OnDroppingWeapon(IWeapon weapon) => 
            weapon.WriteProgress(_persistentData.PlayerProgress);
    }
}