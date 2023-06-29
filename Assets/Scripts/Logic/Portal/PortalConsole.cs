using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using Roguelike.Logic.Interactables;
using Roguelike.Player;
using Roguelike.StaticData.Levels;
using Roguelike.Tutorials;
using Roguelike.UI.Windows;
using Roguelike.UI.Windows.Regions;
using UnityEngine;

namespace Roguelike.Logic.Portal
{
    public class PortalConsole : MonoBehaviour, IInteractable
    {
        private const WindowId RegionSelectionWindowId = WindowId.RegionSelectionWindow;
        
        [SerializeField] private Outline _outline;
        [SerializeField] private Portal _portal;
        
        private IWindowService _windowService;
        private ITutorialService _tutorialService;

        public event Action Interacted;
        
        public Outline Outline => _outline;
        public bool IsActive { get; private set; }

        private void Awake()
        {
            _windowService = AllServices.Container.Single<IWindowService>();
            _tutorialService = AllServices.Container.Single<ITutorialService>();
        }

        private void OnEnable()
        {
            Outline.enabled = false;
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            if (interactor.TryGetComponent(out PlayerHealth player))
            {
                BaseWindow window = _windowService.Open(RegionSelectionWindowId);

                if (window is RegionSelectionWindow regionSelectionWindow)
                    regionSelectionWindow.RegionSelected += OnRegionSelected;
                else
                    throw new NullReferenceException(nameof(window));
                
                Interacted?.Invoke();
            }
        }

        private void OnRegionSelected(RegionSelectionWindow regionSelectionWindow, RegionStaticData regionData)
        {
            _portal.Init(regionData.Id, regionData.Floors[0].Stages[0].Id);
            _tutorialService.TryShowTutorial(TutorialId.Hub10);
        }
    }
}