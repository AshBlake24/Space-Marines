using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.PersistentData;
using UnityEngine;

namespace Roguelike.Loot.Chest
{
    public class WeaponChestObserver : MonoBehaviour
    {
        [SerializeField] private WeaponChest _weaponChest;
        
        private IPersistentDataService _progressData;

        private void Awake() => 
            _progressData = AllServices.Container.Single<IPersistentDataService>();

        private void OnEnable() => 
            _weaponChest.Interacted += OnInteracted;

        private void OnDisable() => 
            _weaponChest.Interacted -= OnInteracted;

        private void OnInteracted() => 
            _progressData.PlayerProgress.Statistics.CollectablesData.ChestsOpened++;
    }
}