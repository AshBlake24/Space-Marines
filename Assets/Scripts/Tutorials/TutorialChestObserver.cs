using Roguelike.Infrastructure.Services;
using Roguelike.Loot.Chest;
using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialChestObserver : MonoBehaviour
    {
        [SerializeField] private SalableWeaponChest _chest;
        [SerializeField] private TutorialId _tutorialWindowId;

        private ITutorialService _tutorialService;
        
        private void Awake() => 
            _tutorialService = AllServices.Container.Single<ITutorialService>();

        private void OnEnable() =>
            _chest.Opened += OnOpened;

        private void OnDisable() => 
            _chest.Opened -= OnOpened;

        private void OnOpened() => 
            _tutorialService.TryShowTutorial(_tutorialWindowId);
    }
}