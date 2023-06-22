using Roguelike.Infrastructure.Services;
using Roguelike.Infrastructure.Services.Windows;
using UnityEngine;

namespace Roguelike.Logic.Interactables
{
    public class StatisticsBoard : MonoBehaviour, IInteractable
    {
        [SerializeField] private Outline _outline;
        
        private IWindowService _windowService;

        public Outline Outline => _outline;
        public bool IsActive { get; private set; }

        private void Awake() => 
            _windowService = AllServices.Container.Single<IWindowService>();

        private void OnEnable()
        {
            Outline.enabled = false;
            IsActive = true;
        }

        public void Interact(GameObject interactor)
        {
            _windowService.Open(WindowId.StatisticsWindow);
        }
    }
}