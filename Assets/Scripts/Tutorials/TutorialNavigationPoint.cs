using System;
using Roguelike.Infrastructure.Services;
using Roguelike.Logic.Interactables;
using UnityEngine;

namespace Roguelike.Tutorials
{
    public class TutorialNavigationPoint : MonoBehaviour
    {
        [SerializeField] private int _routeIndex;
        [SerializeField] private GameObject _interactable;
        [SerializeField] private TutorialId _tutorialWindowId;
        
        private ITutorialService _tutorialService;

        public event Action<int> Interacted;
        
        public int RouteIndex => _routeIndex;

        private void Awake()
        {
            if (_interactable != null)
            {
                _tutorialService = AllServices.Container.Single<ITutorialService>();
                IInteractable interactable = _interactable.GetComponent<IInteractable>();
                interactable.Interacted += OnInteracted;
            }
        }

        private void OnInteracted()
        {
            _tutorialService.TryShowTutorial(_tutorialWindowId);
            Interacted?.Invoke(_routeIndex);
        }
    }
}